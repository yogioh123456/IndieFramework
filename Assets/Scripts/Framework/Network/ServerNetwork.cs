using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class ServerNetwork : Mono {
    private Server server;
    private delegate void ServerMessageReceived(object sender, ServerMessageReceivedEventArgs e);

    private ServerSyncManager serverSyncManager;

    private uint timeTick;
    
    public ServerNetwork() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
        server = new Server();
        server.MessageReceived += OnMessageReceived;
        server.ClientDisconnected += OnClientDisconnected;
    }

    private void OnMessageReceived(object sender, ServerMessageReceivedEventArgs e) {
        Game.GetComp<ServerSyncManager>().AddCmd(sender, e, timeTick);
    }

    private void OnClientDisconnected(object data, ClientDisconnectedEventArgs e) {
        Game.Event.Dispatch("ClientDisconnected", e.Id);
    }
    
    public void StartServer(ushort port, ushort maxNum) {
        server.Start(port, maxNum);
    }

    public override void FixedUpdate() {
        if (server.IsRunning) {
            server.Tick();
            timeTick += 1;
        }
    }

    public override void OnApplicationQuit() {
        server.Stop();
    }

    public void SendToAll(Message message, bool shouldRelease = true) {
        server.SendToAll(message);
    }
    
    public void Send(Message message, ushort clientId,bool shouldRelease = true) {
        server.Send(message, clientId, shouldRelease);
    }
}
