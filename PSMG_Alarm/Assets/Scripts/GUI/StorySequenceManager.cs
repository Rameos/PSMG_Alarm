using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class StorySequenceManager : MonoBehaviour
{
    Text StartGameText;
    public static string introPart;
    public ArrayList intro;
    public Text text;
    private AudioSource StorySequence;

    void Awake()
    {
        StartGameText = GameObject.FindWithTag("StartGameText").GetComponent<Text>();
        StorySequence = GameObject.FindWithTag("StorySequence").GetComponent<AudioSource>() as AudioSource;

        StartGameText.color = new Color(1, 1, 1, 0);
        intro = new ArrayList();

        FileInfo theSourceFile = new FileInfo(Application.dataPath + "/StreamingAssets/Level1.txt");
        StreamReader reader = theSourceFile.OpenText();

        string line;
        int i = 0;

        do
        {
            line = reader.ReadLine();
            intro.Add(line);
            i++;
        } while (line != null);

        StartCoroutine(ShowSentences());
    }

    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            Application.LoadLevel("submarine");
        }
        text.text = introPart;
    }

    public IEnumerator ShowSentences()
    {
        for (int i = 0; i < intro.Count - 1; i++)
        {
            introPart = (intro[i]).ToString();
            yield return new WaitForSeconds(5f);
        }
        StorySequence.mute = true;
        StartGameText.color = new Color (1, 1, 1, 1);
    }

}
