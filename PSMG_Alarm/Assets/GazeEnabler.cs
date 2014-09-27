using UnityEngine;
using System.Collections;

public class GazeEnabler : MonoBehaviour {

    void Start()
    {
        if (!PlayerPrefsManager.GetControl())
            gameObject.SetActive(false);
    }
}
