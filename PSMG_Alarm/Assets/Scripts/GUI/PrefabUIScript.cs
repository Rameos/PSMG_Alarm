using UnityEngine;
using System.Collections;

public class PrefabUIScript : MonoBehaviour {

	public GameObject mainCanvas, multiplayerPanel, clientLobby, serverBrowserCanvas;

	public void GetToClientLobby()
	{
		mainCanvas.SetActive (true);
		multiplayerPanel.SetActive (false);
		clientLobby.SetActive (true);
		serverBrowserCanvas.SetActive (false);
	}
}
