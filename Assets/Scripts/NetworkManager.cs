using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{


    private const string typeName = "CSC404GameJam2";
    private const string gameName = "RoomOne";


    private void StartServer()
    {
        Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
    }

    private HostData[] hostList;

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }


    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
    }


    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(50, 50, 125, 50), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(50, 120, 125, 50), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(200, 50 + (50 * i), 150,50), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }




    // Use this for initialization
    void Start()
    {
        MasterServer.ipAddress = "127.0.0.1";
        MasterServer.port = 23466;
        Network.natFacilitatorIP = "127.0.0.1";
        Network.natFacilitatorPort = 50005;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
