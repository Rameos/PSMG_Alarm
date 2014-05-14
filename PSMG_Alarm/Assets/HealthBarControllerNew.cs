using UnityEngine;
using System.Collections;

public class HealthBarControllerNew : MonoBehaviour {
	
	public GUITexture healthBar;

	private float currentHP;
	private float textureRatio;
	private bool gameOver = false;

	// Use this for initialization
	void Start()
	{
		currentHP = 1;

	}

	void OnGUI() {

	}

	public void takeDamage(float HP) {
		if (HP < 0) {
			HP = 0;
		}
		if (HP != 0) {
			StartCoroutine (AnimateHPLoss (HP));		
		} else {
			StartCoroutine (AnimateHPLoss (HP));
			gameOver = true;
		}

	}


	IEnumerator AnimateHPLoss(float HP){
		while (currentHP > HP) {
			healthBar.pixelInset = (new Rect (0+(float)(Screen.height*0.15*textureRatio*0.1503f), Screen.height-(float)(Screen.height*0.15), (float)(Screen.height*0.15*textureRatio*currentHP), (float)(Screen.height*0.15)));

			currentHP = currentHP - 0.01f;
			yield return new WaitForSeconds(0.03f);
		}
	}

}
