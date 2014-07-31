using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class StorySequenceScript : MonoBehaviour {

	public string[] intro;
	public float off;
	public float speed = 0.001f;
	
	void Start () {
		intro = new string[20];

		FileInfo theSourceFile = new FileInfo ("Assets/StoryAssets/Level1.txt");
		StreamReader reader = theSourceFile.OpenText();
		
		string text;
		int i = 0;
		
		do
		{
			text = reader.ReadLine();
			intro[i] = text;
			i++;
		} while (text != null);  

		off = Screen.height + intro.Length*20;
	}

	void Update () {
		if(Input.GetButton("Escape"))
		   Application.LoadLevel("submarine");
	}

	public void OnGUI()
	{
		off -= Time.deltaTime * speed;
		for (int i = 0; i < intro.Length; i++)
		{
			float roff = (intro.Length*-20) + (i*20 + off);
			float alph = Mathf.Sin((roff/Screen.height)*180*Mathf.Deg2Rad);
			GUI.color = new Color(1,1,1, alph);
			GUI.Label(new Rect(0,roff,Screen.width, 20),intro[i]);
			GUI.color = new Color(1,1,1,1);
		}

		if (off < 300)
			GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "Drücke Escape zum starten!");
	}
}
