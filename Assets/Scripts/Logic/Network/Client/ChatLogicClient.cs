using RiptideNetworking;
using UnityEngine;

public class ChatLogicClient {
    //客户端接收消息
    [MessageHandler((ushort)Msg.chat)]
    private static void PlayerChat(Message message)
    {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("客户端聊天" + playerId + str);
    }
    
    [MessageHandler((ushort)Msg.createPlayer)]
    private static void PlayerCreate(Message message)
    {
        ushort playerId = message.GetUShort();
        Debug.Log("客户端创建玩家" + playerId + "  " + Game.ClientNet.ID);
        if (playerId == Game.ClientNet.ID) {
            Game.GetComp<PlayerManager>().AddPlayer();
        } else {
            Game.GetComp<PlayerManager>().AddOtherPlayer();
        }
    }
}
