using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	public GUIText lifeText;

	private int life;

	// Use this for initialization
	void Start () {
		life = 4;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void incrementLife(){
		life++;
		updateLifeText ();
	}

	public void decrementLife(){
		life--;
		updateLifeText ();
	}

	void updateLifeText(){
		lifeText.text = "x" + life;
	}
}
