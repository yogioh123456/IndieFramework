using RiptideNetworking;
using UnityEngine;

public class ChatLogicClient {
    //客户端接收消息
    [MessageHandler((ushort)MessageTest.MessageTestId.spawnPlayer)]
    private static void SpawnPlayer(Message message)
    {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        
        Debug.Log("生成玩家" + playerId + str);
        
    }
}
