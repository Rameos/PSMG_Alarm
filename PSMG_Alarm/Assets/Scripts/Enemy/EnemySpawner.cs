using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject camera2d;
    public GameObject egg;
    public List<SpawnWave> waves = new List<SpawnWave>();

    public enum enemyType { SMALL_ENEMY, BIG_ENEMY, SEA_MINE }

    private List<SpawnWave> tempRemove = new List<SpawnWave>();
    private List<SpawnWave> tempAdd = new List<SpawnWave>();

    private GameOverScript gameOver;

    void Start()
    {
        gameOver = GameObject.Find("GameController").GetComponent<GameOverScript>();
    }

    void Update()
    {
        if (gameOver.GetGameOver())
        {
            Destroy(this);
        }

        tempRemove.Clear();
        tempAdd.Clear();

        foreach (SpawnWave wave in waves)
        {
            if (wave.time < GameControlScript.timeElapsed)
            {
                if (!wave.spawnAsEgg)
                {
                    for (int i = 0; i < wave.size; i++)
                    {
                        SpawnEnemy(wave.enemy);
                    }
                    tempRemove.Add(wave);

                    if (wave.repeatTime > 0)
                    {
                        tempAdd.Add(new SpawnWave((int)(GameControlScript.timeElapsed + wave.repeatTime), wave.repeatTime, wave.size, wave.enemy, wave.spawnAsEgg));
                    }
                }
                else
                {
                    SpawnEgg(wave.enemy, wave.size);
                    tempRemove.Add(wave);

                    if (wave.repeatTime > 0)
                    {
                        tempAdd.Add(new SpawnWave((int)(GameControlScript.timeElapsed + wave.repeatTime), wave.repeatTime, wave.size, wave.enemy, wave.spawnAsEgg));
                    }
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

    public void SpawnEnemy(GameObject type)
    {
        Vector3 pos = GetRandomPosition();

        NetworkManagerScript.NetworkInstantiate(type, pos, Quaternion.Euler(0, 0, 0));
    }

    public void SpawnEgg(GameObject type, int count)
    {
        Vector3 pos = GetRandomPosition();

        GameObject newEgg = NetworkManagerScript.NetworkInstantiate(egg, pos, Quaternion.Euler(0, 0, 0));
        newEgg.SendMessage("Populate", count);
        newEgg.SendMessage("GiveType", type);
    }

    [System.Serializable]
    public class SpawnWave
    {
        public int time;
        public int repeatTime;
        public int size;
        public GameObject enemy;
        public bool spawnAsEgg;
        public SpawnWave(int time, int repeatTime, int size, GameObject enemy, bool spawnAsEgg)
        {
            this.time = time;
            this.repeatTime = repeatTime;
            this.size = size;
            this.enemy = enemy;
            this.spawnAsEgg = spawnAsEgg;
        }
    }
}