using UnityEngine;
using System.Collections;

public class HealthBarControllerNew : MonoBehaviour {
	
	public GUITexture healthBar;
	public GUIText lifeText;

	private float currentHP;
	private float healthBarLength100;
	private bool gameOver = false;
	private int life = 5;

	// Use this for initialization
	void Start()
	{
		currentHP = 100;
		healthBarLength100 = healthBar.guiTexture.pixelInset.width;
		takeDamage (90);

	}

	void OnGUI() {

	}

	void takeDamage(float dmg) {
		currentHP = currentHP - dmg;

		if (currentHP <= 0) {
			decrementLife();
		}
		if (currentHP != 0) {
			updateHealthbar();
		} else {
			gameOver = true;
		}

	}

	void updateHealthbar (){
		healthBar.guiTexture.pixelInset = new Rect(healthBar.guiTexture.pixelInset.x,healthBar.guiTexture.pixelInset.y, (healthBarLength100 * currentHP/100),healthBar.guiTexture.pixelInset.height);
	}

	void decrementLife() {
		life--;
		updateLifeText ();
	}

	void incrementLife() {
		life++;
		updateLifeText ();
	}
	void updateLifeText() {
		lifeText.text = "x" + life;
	}

	//IEnumerator AnimateHPLoss(float HP){
	//		healthBar.pixelInset = (new Rect (0+(float)(Screen.height*0.15*textureRatio*0.1503f), Screen.height-(float)(Screen.height*0.15), (float)(Screen.height*0.15*textureRatio*currentHP), (float)(Screen.height*0.15)));
	//
	//		currentHP = currentHP - 0.01f;
	//		yield return new WaitForSeconds(0.03f);
	//	}
	//}

}
