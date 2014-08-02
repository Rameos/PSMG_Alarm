using UnityEngine;
using System.Collections;

public class MoveLight : MonoBehaviour
{

    public float speedX = 0.5f;
    public float speedY = 0.5f;

    void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }
}
