using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
	public static bool networkActive = false;

	public string gameName;

	private bool refresh;
	private HostData[] hostData;
	private bool gotHostData;
	
	void Start () {
		gameName = "UFight_test_game";
		refresh = false;
		gotHostData = false;
	}

	void Update () {
		if (refresh) {
			if(MasterServer.PollHostList().Length > 0) {
				refresh = false;
				hostData = MasterServer.PollHostList();
				gotHostData = true;
				Debug.Log ("got host data!");
			}
		}
	}

	public void Server_startServer(){
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer (2, 25000, useNat);
		MasterServer.RegisterHost (gameName, "Noob pwning zone 554","This is the epic first glance in our new Multiplayer");
		Debug.Log (Network.player.ipAddress);
		Debug.Log (Network.player.port);
	}

	public void Client_refreshHostList() {
		MasterServer.RequestHostList (gameName);
		Debug.Log ("start refreshing..");
		refresh = true;
	}

	public HostData[] Client_getHostData() {
		return hostData;
	}

	public bool Client_getHostDataStatus() {
		return gotHostData;
	}

	public void Client_connectToHost(HostData data) { 
		Network.Connect(data);
		//Network.Connect ("132.199.184.52", 25000);
	}

	//Messages
	void OnServerInitialized() {
		Debug.Log ("Server initialized");
	}

	void OnMasterServerEvent(MasterServerEvent mse) {
		Debug.Log ("inmasterserverevent");
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Registered Server!");		
		}
		if (mse == MasterServerEvent.RegistrationFailedNoServer) {
			Debug.Log("Failed. No server!");		
		}
		if (mse == MasterServerEvent.RegistrationFailedGameName) {
			Debug.Log("Failed. Game name error!");		
		}
		if (mse == MasterServerEvent.RegistrationFailedGameType) {
			Debug.Log("Failed. Game type error!");		
		}
		if (mse == MasterServerEvent.HostListReceived) {
			Debug.Log("Host list received!");		
		}
	}

	void OnPlayerConnected(){
		Debug.Log ("player came in!");
		loadLevel ();
	}

	public void loadLevel(){
		if (Network.isServer)
		{
			networkView.RPC("NetworkLoadLevel", RPCMode.AllBuffered, "submarine");
		}
	}

	[RPC]
	void NetworkLoadLevel(string levelName)
	{
		networkActive = true;
		Application.LoadLevel(levelName);
		
		if (Network.isServer)
		{
			Transform root = transform.root;
			recursiveNetworkInstantiate(root);
		}
	}

	private void recursiveNetworkInstantiate(Transform node)
	{
		if (node.networkView != null)
		{
			networkView.RPC("AssignViewID", RPCMode.AllBuffered, node.name, Network.AllocateViewID());
		}
		
		for (int i = 0; i < node.childCount; ++i)
		{
			Transform child = node.GetChild(i);
			recursiveNetworkInstantiate(child);
		}
	}

	[RPC]
	void AssignViewID(string nodeName, NetworkViewID nvid)
	{
		Transform target = transform.root.Find(nodeName);
		if (target != null)
		{
			target.networkView.viewID = nvid;
		}
	}
}
