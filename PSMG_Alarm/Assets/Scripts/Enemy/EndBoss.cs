using UnityEngine;
using System.Collections;

public class EndBoss : Enemy {

	public GameObject eggPrefab;
	public GameObject enemyPrefab;
	public GameObject shieldPrefab;

	public float eggSpawntime;


	private float eggtimer = 0f;
	private float shieldtimer = 0f;
	private bool shieldActive = true;

	public override void Shoot()
	{
		eggtimer += Time.deltaTime;
		
		if (eggtimer > eggSpawntime)
		{
			eggtimer = 0;
			SpawnEgg(enemyPrefab, 5);
		}

		if (shieldActive == false) 
		{
			Debug.Log("shieldactive false" + shieldtimer);
			shieldtimer += Time.deltaTime;
			if(shieldtimer > 10)
			{
				shieldtimer = 0f;
				shieldActive = true;
				SpawnShield(shieldPrefab);
			}
		}

		if (life <= 0) 
		{
			Application.LoadLevel("demo-ende");
		}
	}

	public override void OnTriggerEnter2D(Collider2D col)
	{
		if (!shieldActive) 
		{
			if (col.gameObject.tag == "MachineGun")
			{
				Instantiate(spark, transform.position, transform.rotation);
				col.gameObject.SendMessage("DoDamage", this.gameObject);
			}
			if (col.gameObject.tag == "Rocket")
			{
				Instantiate(explosion, transform.position, transform.rotation);
				col.gameObject.SendMessage("DoDamage", this.gameObject);
			}
			else if (col.gameObject.tag == "Shield")
			{
				player.SendMessage("DestroyShield");
				Destroy(GameObject.Find("Shield(Clone)"));
				col.gameObject.SendMessage("DoDamage", this.gameObject);
			}
			else if (col.gameObject.tag == "Wave")
			{
				col.gameObject.SendMessage("DoDamage", this.gameObject);
			}	
		}
	}

	public void SpawnEgg(GameObject type, int count)
	{
		Vector3 pos = rigidbody2D.position;
		
		GameObject newEgg = NetworkManagerScript.NetworkInstantiate(eggPrefab, pos, Quaternion.Euler(0, 0, 0));
		newEgg.SendMessage("Populate", count);
		newEgg.SendMessage("GiveType", type);
	}

	private void SpawnShield(GameObject shield)
	{
		GameObject newShield = Instantiate (shield, rigidbody2D.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		newShield.transform.parent = gameObject.transform;
	}

	public void setShieldActive(bool value)
	{
		shieldActive = value;
	}

	public bool getShieldActive()
	{
		return shieldActive;
	}
	
}
