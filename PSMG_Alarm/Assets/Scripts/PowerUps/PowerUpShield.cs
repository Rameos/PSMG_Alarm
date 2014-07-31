using UnityEngine;
using System.Collections;

public class PowerUpShield : PowerUp {

	public GameObject shieldPrefab;
	private GameObject player;

    public override void FindOtherObjects()
    {
        player = GameObject.Find("Submarine(Clone)");
    }

    public override void ApplyPowerUp()
    {
        GameObject clone = Instantiate(shieldPrefab, player.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        clone.transform.parent = player.transform;
    }
}
