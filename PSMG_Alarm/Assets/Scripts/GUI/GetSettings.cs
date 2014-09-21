using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetSettings : MonoBehaviour {

    public Slider musicSlider;
    public Slider soundSlider;
    public Button calibrate;
    public Text controlsText;
    private bool controls;

    private bool isActive = false;

	void Start () {
        musicSlider.value = PlayerPrefsManager.GetMusic();
        soundSlider.value = PlayerPrefsManager.GetSound();

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
	
    void OnEnable()
    {
        isActive = true;
    }

    void OnDisable()
    {
        isActive = false;
    }

    public void OnMusicChanged(float value)
    {
        if (!isActive)
        {
            return;
        }
        PlayerPrefsManager.SetMusic(value);
    }

    public void OnSoundChanged(float value)
    {
        if (!isActive)
        {
            return;
        }
        PlayerPrefsManager.SetSound(value);
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
