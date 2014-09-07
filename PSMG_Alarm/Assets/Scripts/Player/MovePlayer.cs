using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public float speedForwards = 20;
    public float speedBackwards = 15;
    public float rotationSpeed = 2;
    public float maxShield = 30;

    private bool moveAllowed = true;
    private float shieldTimer;
    private bool shielded;
    private GameObject shield;
    private SpriteRenderer shieldR;
    private bool pitchUp;

    public GameObject[] lights;

    void Update()
    {
        if (moveAllowed && networkView.isMine && NetworkManagerScript.networkActive) Move();
        if (moveAllowed && NetworkManagerScript.networkActive == false) Move();

        shieldTimer -= Time.deltaTime;

        if (shieldTimer > maxShield)
        {
            shieldTimer = maxShield;
        }

        if (shielded && shield != null)
        {
            shieldR.color = new Color(shieldR.color.r, shieldR.color.g, shieldR.color.b, 0.2f + shieldTimer / maxShield);
        }

        if (shielded && shieldTimer <= 0)
        {
            DestroyShield();
        }
    }

    void Move()
    {
        pitchUp = false;

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

        if (Input.GetButton("Up") || Input.GetButton("Down"))
        {
            pitchUp = true;
        }

        PitchSound();
    }

    public void stopPlayerMovement()
    {
        moveAllowed = false;
    }

    public void startPlayerMovement()
    {
        moveAllowed = true;
    }

    public void LightAnimation()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<Animator>().SetTrigger("TookDamage");
        }
    }

    public bool GetShielded()
    {
        return shielded;
    }

    public void SetShield()
    {
        if (!shielded)
        {
            shielded = true;
            shield = GameObject.FindGameObjectWithTag("Shield");
            shieldR = shield.GetComponent<SpriteRenderer>();
        }

        if (shieldTimer < 0)
        {
            shieldTimer = 20;
        }
        else
        {
            shieldTimer += 20;
        }
    }

    public void DestroyShield()
    {
        if (shielded)
        {
            shielded = false;
            Destroy(shield);
        }

        shieldTimer = 0;
    }

    private void PitchSound()
    {
        if (!pitchUp && GetComponent<AudioSource>().pitch > 1)
        {
            GetComponent<AudioSource>().pitch -= 0.5f * Time.deltaTime;
        }
        else if (pitchUp && GetComponent<AudioSource>().pitch < 2)
        {
            GetComponent<AudioSource>().pitch += 0.5f * Time.deltaTime;
        }
    }
}
