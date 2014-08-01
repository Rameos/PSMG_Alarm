using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    public void Fire(Vector3 aimPosition)
    {
        transform.Rotate(0, 0, 90);
        rigidbody2D.AddForce(transform.right * 1000);
        Destroy(this, 2);
    }
}
