using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour
{
    public static bool networkActive = false;

	public Text statusText;
	public Text serverCall;
	public Text gameDescription;
	public Toggle easy, medium, difficult;

	public string gameName;

    private bool refresh;
    private HostData[] hostData;
    private bool gotHostData;

    void Start()
    {
        gameName = "SubFight";
        refresh = false;
        gotHostData = false;
    }

    void Update()
    {
        if (refresh)
        {
            if (MasterServer.PollHostList().Length != 0)
            {
                refresh = false;
                hostData = MasterServer.PollHostList();
                gotHostData = true;
            }
        }
    }

	//Server methods
	public void Server_startServer()
    {	
		setDifficulty ();
		string serverName = serverCall.text;
		string serverDescription = gameDescription.text;
        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(2, 25000, useNat);
        MasterServer.RegisterHost(gameName, serverName, serverDescription);
		networkActive = true;
    }

	public void Server_UnregisterServer (){
		networkActive = false;
		Network.Disconnect();
		MasterServer.UnregisterHost();
		Debug.Log ("Unregistered Server");
	}

	public void Server_LoadLevel()
	{
		if (Network.isServer)
		{
			networkView.RPC("NetworkLoadLevel", RPCMode.AllBuffered, "submarine");
		}
	}

	private void Server_RecursiveNetworkInstantiate(Transform node)
	{
		if (node.networkView != null)
		{
			networkView.RPC("AssignViewID", RPCMode.AllBuffered, node.name, Network.AllocateViewID());
		}
		
		for (int i = 0; i < node.childCount; ++i)
		{
			Transform child = node.GetChild(i);
			Server_RecursiveNetworkInstantiate(child);
		}
	}

	//Client methods
    public void Client_refreshHostList()
    {
		gotHostData = false;
		MasterServer.ClearHostList ();
        MasterServer.RequestHostList(gameName);
        refresh = true;
    }

    public HostData[] Client_getHostData()
    {
        return hostData;
    }

    public bool Client_getHostDataStatus()
    {
        return gotHostData;
    }

    public void Client_connectToHost(HostData data)
    {
		networkActive = true;
        Network.Connect(data);
    }

	public void Client_disconnectFromHost(){
		networkActive = false;
		Network.CloseConnection(Network.connections[0], true);
		networkView.RPC ("PlayerDisconnected", RPCMode.Server);
	}

    //Messages
    void OnServerInitialized()
    {
		statusText.text = "Server initialized.. Waiting for player to join!";
    }

	public void resetStatusMessage(){
		statusText.text = "";
	}

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server!");
        }
        if (mse == MasterServerEvent.RegistrationFailedNoServer)
        {
            Debug.Log("Failed. No server!");
        }
        if (mse == MasterServerEvent.RegistrationFailedGameName)
        {
            Debug.Log("Failed. Game name error!");
        }
        if (mse == MasterServerEvent.RegistrationFailedGameType)
        {
            Debug.Log("Failed. Game type error!");
        }
        if (mse == MasterServerEvent.HostListReceived)
        {
            Debug.Log("Host list received!");
			gotHostData = true;
        }
    }

    void OnPlayerConnected()
    {
        Debug.Log("player came in!");
		//Menu.EnableStartButton ();
		//Menu.ChangeHostFeedBackText("A player joined the game! You can now start");
    }
    
	//RPC calls
    [RPC]
    void NetworkLoadLevel(string levelName)
    {
        networkActive = true;
        Application.LoadLevel(levelName);

        if (Network.isServer)
        {
            Transform root = transform.root;
		Server_RecursiveNetworkInstantiate(root);
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

	[RPC]
	void PlayerDisconnected(){
		//Menu.ChangeHostFeedBackText ("Player disconnected! Waiting for player to connect...");
		//Menu.DisableStartButton();
	}

	//Other stuff
	public void setDifficulty(){
		if (easy.isOn) {
			PlayerPrefsManager.SetDifficulty(0);
		}
		if (medium.isOn) {
			PlayerPrefsManager.SetDifficulty(1);
		}
		if (difficult.isOn) {
			PlayerPrefsManager.SetDifficulty(2);
		}
	}
}
