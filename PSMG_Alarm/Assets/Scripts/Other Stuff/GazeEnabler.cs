using UnityEngine;
using System.Collections;

public class GazeEnabler : MonoBehaviour {

    void Awake()
    {
        if (!PlayerPrefsManager.GetControl())
            gameObject.SetActive(false);
    }
}
