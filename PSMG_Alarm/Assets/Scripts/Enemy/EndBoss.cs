using UnityEngine;
using System.Collections;

public class EndBoss : Enemy {

	public GameObject eggPrefab;
	public GameObject enemyPrefab;

	public float eggSpawntime;


	private float time = 0f;

	public override void Shoot()
	{
		time += Time.deltaTime;
		
		if (time > eggSpawntime)
		{
			time = 0;
			SpawnEgg(enemyPrefab, 5);
		}

		if (life <= 0) 
		{
			Application.LoadLevel("demo-ende");
		}
	}

	public override void OnTriggerEnter2D(Collider2D col)
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



	public void SpawnEgg(GameObject type, int count)
	{
		Vector3 pos = rigidbody2D.position;
		
		GameObject newEgg = NetworkManagerScript.NetworkInstantiate(eggPrefab, pos, Quaternion.Euler(0, 0, 0));
		newEgg.SendMessage("Populate", count);
		newEgg.SendMessage("GiveType", type);
	}
	
}
