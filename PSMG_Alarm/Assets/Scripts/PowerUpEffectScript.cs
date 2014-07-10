using UnityEngine;
using System.Collections;

public class PowerUpEffectScript : MonoBehaviour {

	public GameObject shieldPrefab;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shield() {
		Vector3 ubootposition = Camera.main.WorldToScreenPoint(transform.position);
		GameObject clone = Instantiate(shieldPrefab, player.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		clone.transform.parent = player.transform;
	}

   
}
