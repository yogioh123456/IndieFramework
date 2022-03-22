using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class ClientNetworkManager : MonoBehaviour {
    public string ip;
    public ushort port;
    public Client client;
    private bool isStart;
    
    // Start is called before the first frame update
    public void Client()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
        client = new Client();
        client.Connect($"{ip}:{port}");
    }

    private void FixedUpdate() {
        client?.Tick();
    }
    
    private void OnApplicationQuit() {
        client.Disconnect();
    }
}
