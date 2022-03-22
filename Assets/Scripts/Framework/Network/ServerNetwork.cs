using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class ServerNetwork : Mono {
    private Server server;
    
    public ServerNetwork() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
        server = new Server();
    }

    public void StartServer(ushort port, ushort maxNum) {
        server.Start(port, maxNum);
    }

    public override void FixedUpdate() {
        server?.Tick();
    }

    public override void OnApplicationQuit() {
        server.Stop();
    }
}
