using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class StorySequenceScript : MonoBehaviour
{
    public ArrayList intro;
    public float off;
    public float speed = 0.001f;

    public GameObject SpeechBubble;
    public GameObject speechBubble;
    public string introText;
    private GUIText SpeechBubbleText;
    private int currentLine;
    private bool showIntroText;

    void Start()
    {
        introText = "";
        showIntroText = true;

        intro = new ArrayList();

        FileInfo theSourceFile = new FileInfo("Assets/StoryAssets/Level1.txt");
        StreamReader reader = theSourceFile.OpenText();
        speechBubble = (GameObject)Instantiate(SpeechBubble);
        SpeechBubbleText = GameObject.FindWithTag("SpeechBubbleText").GetComponent<GUIText>() as GUIText;
        string line;
        int i = 0;
        currentLine = 0;

        do
        {
            line = reader.ReadLine();
            intro.Add(line);
            i++;
        } while (line != null);

        do
        {
            if (currentLine >= 5)
            {
                showIntroText = false;
            }

            SpeechBubbleText.text = (intro[currentLine]).ToString();
            StartCoroutine(HideSpeechBubble());
        } while (showIntroText == true);
    }

    void Update()
    {
        if (Input.GetButton("Escape"))
            Application.LoadLevel("submarine");
    }

    public IEnumerator HideSpeechBubble()
    {
        yield return new WaitForSeconds(8f);
        currentLine++;
    }
}
