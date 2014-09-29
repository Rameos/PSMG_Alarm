using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServlistitemScript : MonoBehaviour {

	public Text gameName;
	public Text gameDifficulty;

	private HostData hostData;
	private GameObject networkManager;

	// Use this for initialization
	void Start () 
	{
		networkManager = GameObject.Find("Net Work Manager");
	}

	public void SetGameName(string input)
	{
		gameName.text = input;
	}
	public void SetGameDifficulty(string input)
	{
		gameDifficulty.text = input;
	}
	public void SetHostData(HostData hostDataNew)
	{
		hostData = hostDataNew;
	}
	public void ConnectToHost()
	{
		networkManager.GetComponent<NetworkManagerScript> ().Client_connectToHost (hostData);
		GameObject.Find ("PrefabUIManager").GetComponent<PrefabUIScript> ().GetToClientLobby ();
	}


}
