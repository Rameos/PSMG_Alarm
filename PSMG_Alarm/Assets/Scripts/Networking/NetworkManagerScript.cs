using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour
{
    public static bool networkActive = false;

	public Menu Menu;

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
                Debug.Log("got host data!");
            }
        }
    }

    public static GameObject NetworkInstantiate(GameObject initObject, Vector3 position, Quaternion rotation,
        bool spawnOnServer = false, bool hasToBeMine = false)
    {
        if (networkActive)
        {
            if (hasToBeMine)
            {
                if (spawnOnServer && initObject.networkView.isMine)
                {
                    if (Network.isServer)
                        return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
                }
                else if (initObject.networkView.isMine)
                {
                    return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
                }
            }
            else
            {
                if (spawnOnServer)
                {
                    if (Network.isServer)
                        return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
                }
                else
                {
                    return (GameObject)Network.Instantiate(initObject, position, rotation, 5);
                }
            }
        }
        else
        {
            return (GameObject)Instantiate(initObject, position, rotation);
        }

        return null;
    }

	public void Server_startServer(string serverName,string serverDescription)
    {
        bool useNat = !Network.HavePublicAddress();
        Network.InitializeServer(2, 25000, useNat);
        MasterServer.RegisterHost(gameName, serverName, serverDescription);
    }

	public void Server_UnregisterServer (){
		Network.Disconnect();
		MasterServer.UnregisterHost();
	}

    public void Client_refreshHostList()
    {
		gotHostData = false;
		MasterServer.ClearHostList ();
        MasterServer.RequestHostList(gameName);
        Debug.Log("start refreshing..");
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
        Network.Connect(data);
        //Network.Connect ("132.199.184.52", 25000);
    }

    //Messages
    void OnServerInitialized()
    {
        Debug.Log("Server initialized");
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        Debug.Log("inmasterserverevent");
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
			Menu.ChangeHostFeedBackText("Server Registered.. Waiting for player to join!");
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
		Menu.EnableStartButton ();
		Menu.ChangeHostFeedBackText("A player joined the game! You can now start");
    }

    public void Server_LoadLevel()
    {
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
		Server_RecursiveNetworkInstantiate(root);
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
