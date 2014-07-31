using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyRed;
    public GameObject camera2d;
    public List<SpawnWave> waves = new List<SpawnWave>();

    public enum enemyType { smallEnemy, bigEnemy }

    private List<SpawnWave> tempRemove = new List<SpawnWave>();
    private List<SpawnWave> tempAdd = new List<SpawnWave>();

    void Update()
    {
        tempRemove.Clear();
        tempAdd.Clear();

        foreach (SpawnWave wave in waves)
        {
            if (wave.time < GameControlScript.timeElapsed)
            {
                for (int i = 0; i < wave.size; i++)
                {
                    SpawnEnemy(wave.type);
                }
                tempRemove.Add(wave);

                if (wave.repeatTime > 0)
                {
                    tempAdd.Add(new SpawnWave((int)(GameControlScript.timeElapsed + wave.repeatTime), wave.repeatTime, wave.size, wave.type));
                }
            }
        }

        foreach (SpawnWave wave in tempAdd)
        {
            waves.Add(wave);
        }

        foreach (SpawnWave wave in tempRemove)
        {
            waves.Remove(wave);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float screenX;
        float screenY;

        int edge = Random.Range(0, 4);

        if (edge == 0)
        {
            screenX = Random.Range(camera2d.camera.pixelWidth, camera2d.camera.pixelWidth + 2);
            screenY = Random.Range(0, -1);
        }
        else if (edge == 1)
        {
            screenX = Random.Range(0, -2);
            screenY = Random.Range(camera2d.camera.pixelHeight, camera2d.camera.pixelHeight + 2);
        }
        else if (edge == 2)
        {
            screenX = Random.Range(0, -2);
            screenY = Random.Range(0, -2);
        }
        else
        {
            screenX = Random.Range(camera2d.camera.pixelWidth, camera2d.camera.pixelWidth + 2);
            screenY = Random.Range(camera2d.camera.pixelHeight, camera2d.camera.pixelHeight + 2);
        }
        Vector3 randomPos = camera2d.camera.ScreenToWorldPoint(new Vector3(screenX, screenY, 9));
        randomPos.z = 9;

        return randomPos;
    }

    public void SpawnEnemy(enemyType type)
    {
        Vector3 pos = GetRandomPosition();

        if (NetworkManagerScript.networkActive && Network.isServer)
        {
            switch (type)
            {
                case enemyType.bigEnemy:
                    Network.Instantiate(enemyRed, pos, Quaternion.Euler(0, 0, 0), 6);
                    break;
                case enemyType.smallEnemy:
                    Network.Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0), 5);
                    break;
            }
        }
        if (NetworkManagerScript.networkActive == false)
        {
            switch (type)
            {
                case enemyType.bigEnemy:
                    Instantiate(enemyRed, pos, Quaternion.Euler(0, 0, 0));
                    break;
                case enemyType.smallEnemy:
                    Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
    }

    [System.Serializable]
    public class SpawnWave
    {
        public int time;
        public int repeatTime;
        public int size;
        public EnemySpawner.enemyType type;
        public SpawnWave(int time, int repeatTime, int size, EnemySpawner.enemyType type)
        {
            this.time = time;
            this.repeatTime = repeatTime;
            this.size = size;
            this.type = type;
        }
    }
}