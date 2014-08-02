using UnityEngine;
using System.Collections;

public class SwingRandom : MonoBehaviour {

    private Vector3 target;
    private float speed;

	void Start () {
        speed = Random.Range(-1f, 1f);

        target = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));

        rigidbody2D.AddForce(target * 10);
	}
	void Update () {
        transform.Rotate(0, 0, speed);
	}
}
