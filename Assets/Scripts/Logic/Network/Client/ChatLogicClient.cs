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
            Game.GetComp<PlayerManager>().AddPlayer(playerId);
        } else {
            Game.GetComp<PlayerManager>().AddOtherPlayer(playerId);
        }
    }
    
    [MessageHandler((ushort)Msg.connected)]
    private static void PlayerConnected(Message message)
    {
        Debug.Log("连接服务器成功");
        Game.ClientNet.Send(Msg.createPlayer);
    }
    
    [MessageHandler((ushort)Msg.playerMove)]
    private static void PlayerMove(Message message)
    {
        ushort id = message.GetUShort();
        Vector3 pos = message.GetVector3();
        Vector3 dir = message.GetVector3();
        Game.GetComp<PlayerManager>().SetPlayerPos(id, pos, dir);
    }
}
