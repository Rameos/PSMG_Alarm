using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public float startingShakeDistance = 0.8f;
    public float decreasePercentage = 0.5f;
    public float shakeSpeed = 50;
    public int numberOfShakes = 10;

    private float hitTime;
    private float shakeDistance;
    private Vector3 originalPosition;
    private int shake;
    private bool isShaking;

    void Update()
    {
        if(isShaking)
        {
            float timer = (Time.time - hitTime) * shakeSpeed;
            transform.localPosition = new Vector3(originalPosition.x + Mathf.Sin(timer) * shakeDistance, transform.position.y, transform.position.z);

            if (timer > Mathf.PI * 2)
            {
                hitTime = Time.time;
                shakeDistance *= decreasePercentage;
                shake--;
            }

            if (shake == 0)
            {
                isShaking = false;
                transform.localPosition = originalPosition;
            }
        }
    }

    public void StartShaking(float damp)
    {
        isShaking = true;

        hitTime = Time.time;
        shakeDistance = startingShakeDistance * damp;
        originalPosition = transform.position;
        shake = numberOfShakes;
    }
}
