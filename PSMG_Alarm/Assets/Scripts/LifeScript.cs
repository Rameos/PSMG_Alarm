using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	public GUIText lifeText;

	private int life;

	// Use this for initialization
	void Start () {
		life = 5;
		decrementLife ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void incrementLife(){
		life++;
		updateLifeText ();
	}

	void decrementLife(){
		life--;
		updateLifeText ();
	}

	void updateLifeText(){
		lifeText.text = "x" + life;
	}
}
