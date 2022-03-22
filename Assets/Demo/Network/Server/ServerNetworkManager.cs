using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

namespace GameServer {
    public class ServerNetworkManager : MonoBehaviour {
        public ushort port;
        public ushort maxClientCount;
        public Server server;
        
        public void StartServer() {
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
            server = new Server();
            server.Start(port, maxClientCount);
        }

        private void FixedUpdate() {
            server?.Tick();
        }

        private void OnApplicationQuit() {
            server.Stop();
        }
    }
}