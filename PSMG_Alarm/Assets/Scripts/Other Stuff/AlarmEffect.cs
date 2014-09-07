using UnityEngine;
using System.Collections;

public class AlarmEffect : MonoBehaviour
{
    public float minIntensity;
    public float maxIntensity;
    public float speed = 1f;

    private float multiplier = 1;

    void Update()
    {
        light.intensity += speed * Time.deltaTime * multiplier;

        if (light.intensity > maxIntensity)
            multiplier = -1;
        if (light.intensity < minIntensity)
            multiplier = 1;
    }
}
