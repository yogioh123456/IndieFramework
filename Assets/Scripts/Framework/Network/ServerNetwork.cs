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
        server.Start(7777, 10);
    }

    public override void FixedUpdate() {
        server?.Tick();
    }

    public override void OnApplicationQuit() {
        server.Stop();
    }
}
