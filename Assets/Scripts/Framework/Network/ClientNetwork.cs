using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class ClientNetwork : Mono
{
    private Client client;
    
    public ClientNetwork() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
        client = new Client();
    }

    public override void FixedUpdate() {
        client?.Tick();
    }

    public override void OnApplicationQuit() {
        client.Disconnect();
    }

    public void Connect(string ip, ushort port) {
        client.Connect($"{ip}:{port}");
    }
}
