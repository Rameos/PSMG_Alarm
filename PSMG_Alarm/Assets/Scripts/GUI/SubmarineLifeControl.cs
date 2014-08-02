﻿using UnityEngine;
using System.Collections;

public class SubmarineLifeControl : MonoBehaviour {

	public GUITexture [] sub = new GUITexture[4];
	public Texture2D red;
	public Texture2D grey;
	public GameOverScript gameOverScript;

    private GameObject player;
	private int [] lifeArray = new int[4];
	private int life;
	void Start () {
		life = 4;
		for (int i = 0; i < lifeArray.Length; i++) {
			lifeArray[i] = 1;
		}
		updateLife ();

        player = GameObject.FindGameObjectWithTag("Player");
	}

	public void incrementLife(){
		if (life > 0 && life < 4) {
			life++;
			lifeArray [life - 1] = 1;
			updateLife ();
		}
	}

	public void decrementLife(){
		if (life > 0 && life <= 4) {
			life--;
			lifeArray[life] = 0;
			updateLife ();

            player.SendMessage("LightAnimation");
		}
		if (life <= 0) {
			gameOverScript.endOfGame();
		}
	}

	void updateLife() {

		for (int i = 0; i < lifeArray.Length; i++) {
			if(lifeArray[i] == 1){
				sub[i].texture = red;
			}
			else{
				sub[i].texture = grey;
			}
		}

	}

}