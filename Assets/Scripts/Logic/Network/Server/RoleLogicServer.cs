using RiptideNetworking;
using UnityEngine;

public class RoleLogicServer {
    //服务端处理消息
    [MessageHandler((ushort)Msg.chat)]
    private static void PlayerChat(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("服务端聊天" + playerId + str);
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.chat);
        messageToSend.AddUShort(playerId);
        messageToSend.AddString(str);
        Game.ServerNet.SendToAll(messageToSend);
    }

    //创建玩家
    [MessageHandler((ushort)Msg.createPlayer)]
    public static void SpawnPlayer(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        
        Debug.LogError("服务端生成玩家" + fromClientId);
        
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.createPlayer);
        messageToSend.AddUShort(playerId);
        Game.ServerNet.SendToAll(messageToSend);
        //
        Game.GetComp<ServerSyncManager>().AddCmdMsg(0, Msg.createPlayer, playerId);
        
        //保存服务器状态
        PlayerServerData player = new PlayerServerData();
        player.id = playerId;
        Game.ServerMain.serverRoleManager.AddPlayer(playerId, player);
    }
    
    //玩家移动
    [MessageHandler((ushort) Msg.playerMove)]
    public static void PlayerMove(ushort fromClientId, Message message) {
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.playerMove);

        ushort playerId = message.GetUShort();
        Vector3 pos = message.GetVector3();
        Vector3 dir = message.GetVector3();
        
        messageToSend.AddUShort(playerId);
        messageToSend.AddVector3(pos);
        messageToSend.AddVector3(dir);
        Game.ServerNet.SendToAll(messageToSend);
        //
        Game.GetComp<ServerSyncManager>().AddCmdMsg(0, Msg.playerMove, playerId, pos, dir);
    }
}