using UnityEngine;
using System.Collections;

public class SubmarineLifeControl : MonoBehaviour {

	public GUITexture [] sub = new GUITexture[4];
	public GameOverScript gameOverScript;
	public Texture2D red;
	public Texture2D grey;

    private GameObject player;
	private int [] lifeArray = new int[4];
	private int life = 4;
	void Start () {
		for (int i = 0; i < lifeArray.Length; i++) {
			lifeArray[i] = 1;
		}

        int currentLife = PlayerPrefsManager.GetCurrentLife();
        if (currentLife == 0)
        {
            PlayerPrefsManager.SetCurrentLive(4);
            currentLife = 4;
        }

        Debug.Log(currentLife + " " + life);

        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < life - currentLife; i++)
        {
            DecrementLife();
        }

		UpdateLife ();
	}

	public void IncrementLife(){
		if (life > 0 && life < 4) {
			life++;
			lifeArray [life - 1] = 1;
			UpdateLife ();
		}
	}

	public void DecrementLife(){
		if (life > 0 && life <= 4) {
			life--;
			lifeArray[life] = 0;
			UpdateLife ();

            player.SendMessage("LightAnimation");
		}
		if (life <= 0) {
			gameOverScript.endOfGame();
		}
	}

	void UpdateLife() {

		for (int i = 0; i < lifeArray.Length; i++) {
			if(lifeArray[i] == 1){
				sub[i].texture = red;
			}
			else{
				sub[i].texture = grey;
			}
		}
	}

    public int GetLifes()
    {
        return life;
    }

}
