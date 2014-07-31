using UnityEngine;
using System.Collections;

public class NewShoGazeData : MonoBehaviour {
    public Texture2D gazeCursor;
	// Use this for initialization

    void OnGUI()
    {
        float left = (gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x)*0.5f;
        float top = (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f;
        float width = 100;
        float height = 100;
        Rect positionGazeCursor = new Rect(left, top, width, height);

        GUI.DrawTexture(positionGazeCursor, gazeCursor);

    }
	void Start () {
        Debug.Log("GazeData left Eye");
        Debug.Log("GazeLeftEye: " + gazeModel.posGazeLeft);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
