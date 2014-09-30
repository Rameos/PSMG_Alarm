using UnityEngine;
using System.Collections;

public class EndBossShield : MonoBehaviour {
	float timestamp = 0f;

	private SubmarineLifeControl submarineLifeControl;

	// Use this for initialization
	void Start () {
		submarineLifeControl = GameObject.Find ("GameController").GetComponent<SubmarineLifeControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//set timestamp to avoid multipledamage
		if (Time.time - timestamp < 0.1f)
		{
			return;
		}
		timestamp = Time.time;
		
		if (col.gameObject.tag == "Player")
		{
			foreach (ContactPoint2D contacts in col.contacts)
			{
				submarineLifeControl.DecrementLife();
				col.gameObject.rigidbody2D.AddForce(contacts.normal * 3000);
			}
		}
	}
}
