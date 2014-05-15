using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    int x = 0;
    int y = 0;
    int speed = 20;
    bool keyPressed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    if (y < 0) y = -y;
        //    y += speed;
        //    keyPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    if (y > 0) y = -y;
        //    y -= speed;
        //    keyPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    if (x > 0) x = -x;
        //    x -= speed;
        //    keyPressed = true;
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (x < 0) x = -x;
        //    x += speed;
        //    keyPressed = true;
        //}
        //if (keyPressed == true)
        //{
        //    transform.Translate(x * Time.deltaTime / 10, y * Time.deltaTime / 10, 0);
        //}
        //if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    keyPressed = false;
        //}
        //if (keyPressed == false)
        //{
        //    if (y < 0) y += 1;
        //    if (y > 0) y -= 1;
        //    if (x > 0) x -= 1;
        //    if (x < 0) x += 1;
        //}

        if (Input.GetAxis("Vertical") > 0) 
        {
            rigidbody2D.AddForce(transform.right * 20);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody2D.AddForce(-transform.right * 20);
        }

        if (Input.GetButton("Left"))
        {
            transform.Rotate(Vector3.forward);
        }

        if (Input.GetButton("Right"))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
