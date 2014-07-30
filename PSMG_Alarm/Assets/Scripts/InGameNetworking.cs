using UnityEngine;
using System.Collections;

public class InGameNetworking : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject player2Prefab;
	public Transform spawn1;
	public Transform spawn2;

	// Use this for initialization
	void Start () {
		if (NetworkManagerScript.networkActive) {
			spawnPlayer ();
		} else {
			Instantiate(playerPrefab, spawn1.position, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnPlayer () {
		Debug.Log ("spawnin player");
		if (Network.isServer) {
			Network.Instantiate (playerPrefab, spawn1.position,  Quaternion.identity, 0);
		} else {
			Network.Instantiate(player2Prefab, spawn2.position,  Quaternion.identity, 1);	
		}

			

	}
}
