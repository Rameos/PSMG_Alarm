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
			NetworkInstantiate(playerPrefab, spawn1.position, Quaternion.identity, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnPlayer () {
            NetworkInstantiate(playerPrefab, spawn1.position, Quaternion.identity, true);
            NetworkInstantiate(player2Prefab, spawn2.position, Quaternion.identity, false);
	}

	public static GameObject NetworkInstantiate(GameObject initObject, Vector3 position, Quaternion rotation,
	                                            bool spawnOnServer)
	{
		if (NetworkManagerScript.networkActive)
		{
			if (spawnOnServer && Network.isServer){
				return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
			}
			if(spawnOnServer == false && Network.isClient) {
				return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
			}
		}
		else
		{
			return (GameObject)Instantiate(initObject, position, rotation);
		}
		
		return null;
	}
}
