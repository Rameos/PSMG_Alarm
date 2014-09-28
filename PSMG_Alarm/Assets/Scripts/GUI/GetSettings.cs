using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetSettings : MonoBehaviour {
    public Button calibrate;
    public Text controlsText;
    private bool controls;

	void Start () {
        if (PlayerPrefsManager.GetControl() == true)
        {
            controlsText.text = "ZIELEN: BLICK";
            calibrate.interactable = true;
            controls = true;
        }
        else
        {
            controlsText.text = "ZIELEN: MAUS";
            calibrate.interactable = false;
            controls = false;
        }
	}

    public void OnControlsChanged()
    {
        if (controls)
        {
            PlayerPrefsManager.SetControl(false);
            controlsText.text = "ZIELEN: MAUS";
            calibrate.interactable = false;
            controls = false;
        }
        else
        {
            PlayerPrefsManager.SetControl(true);
            controlsText.text = "ZIELEN: BLICK";
            calibrate.interactable = true;
            controls = true;
        }
    }
}
