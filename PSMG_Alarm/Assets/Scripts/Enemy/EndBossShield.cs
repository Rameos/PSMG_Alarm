using UnityEngine;
using System.Collections;

public class EndBossShield : MonoBehaviour {
	float timestamp = 0f;

	private float shieldValue = 30f;

	public int shieldDrain = 10;
	private EndBoss bossScript;

	private SubmarineLifeControl submarineLifeControl;

	// Use this for initialization
	void Start () {
		submarineLifeControl = GameObject.Find ("Lifebar").GetComponent<SubmarineLifeControl> ();
		bossScript = GameObject.Find ("Endboss").GetComponent<EndBoss> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shieldValue <= 0) {
			DestroyEBShield();		
		}
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

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "XRay")
		{
			shieldValue -= Time.deltaTime * shieldDrain;
			SpriteRenderer shieldR = GetComponent<SpriteRenderer>();
			shieldR.color = new Color(shieldR.color.r, shieldR.color.g, shieldR.color.b, 0.2f + shieldValue/30);
		}
	}

	public void DestroyEBShield()
	{
		bossScript.setShieldActive (false);
		Destroy (this.gameObject);
	}
}
