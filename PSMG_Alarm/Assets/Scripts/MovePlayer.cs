using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public float speedForwards = 20;
    public float speedBackwards = 15;
    public float rotationSpeed = 2;

    private bool moveAllowed = true;

    void Start()
    {

    }

    void Update()
    {
		if(moveAllowed && networkView.isMine && NetworkManagerScript.networkActive) Move();
		if(moveAllowed && NetworkManagerScript.networkActive==false) Move ();
    }

    void Move()
    {
		
        if (Input.GetAxis("Vertical") > 0) 
        {
            rigidbody2D.AddForce(transform.right * speedForwards);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody2D.AddForce(-transform.right * speedBackwards);
        }

        if (Input.GetButton("Left"))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        if (Input.GetButton("Right"))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
		
    }

    public void stopPlayerMovement() {
        moveAllowed = false;
    }

	public void startPlayerMovement() {
		moveAllowed = true;
	}
}
