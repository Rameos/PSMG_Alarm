using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class StorySequenceScript : MonoBehaviour {

	//public string[] intro;
	public ArrayList intro;
	public float off;
	public float speed = 0.001f;

	public GameObject SpeechBubble;
	public GameObject speechBubble;
	public string introText;
	private GUIText SpeechBubbleText;
	private int currentLine; 
	private bool showIntroText;
	
	void Start () {
		//TextMesh IntroGUI = new TextMesh ();
		introText = "";

		intro = new ArrayList (); //string[20];

		FileInfo theSourceFile = new FileInfo ("Assets/StoryAssets/Level1.txt");
		StreamReader reader = theSourceFile.OpenText();
		speechBubble = (GameObject)Instantiate(SpeechBubble);
		//SpeechBubbleText = GameObject.FindWithTag("SpeechBubbleText").GetComponent<GUIText>() as GUIText;
		SpeechBubbleText = GameObject.FindWithTag("SpeechBubbleText").GetComponent<GUIText>() as GUIText;

		/*GameObject SpeechBubbleText = new GameObject ();
		SpeechBubbleText.AddComponent<GUIText> ();
		SpeechBubbleText.transform.position = new Vector3(0.1f, 0.5f, 45f);
		SpeechBubbleText.guiText.text = "hi";*/
		//showSpeechBubbles = true;

		string line;
		int i = 0;
		currentLine = 0; 
		
		do
		{
			line = reader.ReadLine();
			intro.Add (line);
			i++;
		} while (line != null); 

		showIntroText = true;
		if (currentLine == intro.Count-1)
			showIntroText = false;

		while (showIntroText == true) {
			SpeechBubbleText.text = (intro [currentLine]).ToString ();
			StartCoroutine (HideSpeechBubble ());
		}
		/*foreach(string j in intro) {
			//IntroGUI.text = j;
			Debug.Log (j);
			//introText = j;
			SpeechBubbleText.guiText.text = j;
			//SpeechBubbleText.text = j;
			StartCoroutine (HideSpeechBubble());
		}*/

		//off = Screen.height + intro.Length*20;
	}

	void Update () {
		if(Input.GetButton("Escape"))
		   Application.LoadLevel("submarine");
	}


	public void OnGUI()
	{
		GUI.color = Color.gray;

		//GameObject speechBubble = Instantiate (Resources.Load ("SpeachBubble"));

		GUIStyle labelStyle = new GUIStyle (GUI.skin.label);
		//StartCoroutine (HideSpeechBubble());


		labelStyle.fontSize = 12;

		off -= Time.deltaTime * speed;
		//GUI.TextArea (new Rect (100, 200, Screen.width/2, Screen.height), "hi", labelStyle);
		//float roff = (intro.Length*-20)+ (Screen.height + 140);
	//if(showSpeechBubbles) {

		/*foreach(string j in intro) 
		{
			introText = j;
			//introText = intro[i];
			//float roff = (intro.Length*-20) + (i*20 + off);
			//float roff = (intro.Length*-20)+ (i*20 + Screen.height + 140);
			//float alph = Mathf.Sin((roff/Screen.height)*180*Mathf.Deg2Rad);
			//GUI.color = new Color(1,1,1, alph);
			//GUI.Label(new Rect(20,roff,Screen.width, 20),intro[i], labelStyle);
			//GUI.Label(new Rect(5,roff,Screen.width, 20),intro[i], labelStyle);
			GUI.TextArea (new Rect (100, 200, Screen.width/2, Screen.height), introText, labelStyle);
			StartCoroutine (HideSpeechBubble());
		}*/

		//}
		//GUI.TextArea (new Rect (5, 180, Screen.width/2, Screen.height), intro[i], labelStyle);
		//GUI.Label(new Rect(5,roff,Screen.width, 20),text, labelStyle);

		if (off < 300)
			GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "Drücke Escape zum starten!", labelStyle);
	}

	public IEnumerator HideSpeechBubble() {
		//GameObject speechBubble = (GameObject)Instantiate(SpeechBubble);
		yield return new WaitForSeconds (7f);
		//Destroy (speechBubble);
		//speechBubble.renderer.enabled = false;
		currentLine++;

		//showSpeechBubbles = false;
	}
}
