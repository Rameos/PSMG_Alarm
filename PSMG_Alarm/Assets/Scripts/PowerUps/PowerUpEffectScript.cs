using UnityEngine;
using System.Collections;

public class PowerUpEffectScript : MonoBehaviour {

	public GameObject shieldPrefab;
	public GameObject player;

	public void shield() {
		GameObject clone = Instantiate(shieldPrefab, player.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		clone.transform.parent = player.transform;
	}

   
}
