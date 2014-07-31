using UnityEngine;
using System.Collections;

public class AlarmEffect : MonoBehaviour
{

    public float minIntensity;
    public float maxIntensity;
    public float speed = 1f;

    void Update()
    {
        light.intensity += speed * Time.deltaTime;

        if (light.intensity > maxIntensity || light.intensity < minIntensity)
            speed *= -1;
    }
}
