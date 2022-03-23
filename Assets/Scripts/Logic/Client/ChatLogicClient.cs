using RiptideNetworking;
using UnityEngine;

public class ChatLogicClient {
    //客户端接收消息
    [MessageHandler((ushort)Msg.chat)]
    private static void SpawnPlayer(Message message)
    {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        
        Debug.Log("客户端生成玩家" + playerId + str);
        
    }
}
