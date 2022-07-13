using RiptideNetworking;
using UnityEngine;

public class RoleLogicServer {
    //服务端处理消息
    [MessageHandler((ushort)Msg.Chat)]
    private static void PlayerChat(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("服务端聊天" + playerId + str);
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.Chat);
        messageToSend.AddUShort(playerId);
        messageToSend.AddString(str);
        Game.ServerNet.SendToAll(messageToSend);
    }

    //创建玩家
    [MessageHandler((ushort)Msg.CreatePlayer)]
    public static void SpawnPlayer(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        
        Debug.LogError("服务端生成玩家" + fromClientId);
        
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.CreatePlayer);
        messageToSend.AddUShort(playerId);
        Game.ServerNet.SendToAll(messageToSend);

        //保存服务器状态
        PlayerNetData player = new PlayerNetData();
        player.id = playerId;
        Game.ServerMain.serverRoleManager.AddPlayer(playerId, player);
    }
    
    //创建玩家
    [MessageHandler((ushort)Msg.RoleState)]
    public static void RoleState(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        byte state = message.GetByte();
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.RoleState);
        messageToSend.AddUShort(playerId);
        messageToSend.AddUShort(state);
        Game.ServerNet.SendToAll(messageToSend);

        Game.ServerMain.serverRoleManager.SetPlayerState(playerId, state);
    }
    
    //玩家移动
    [MessageHandler((ushort) Msg.PlayerMove)]
    public static void PlayerMove(ushort fromClientId, Message message) {
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.PlayerMove);

        ushort playerId = message.GetUShort();
        Vector3 pos = message.GetVector3();
        Vector3 dir = message.GetVector3();
        
        Game.ServerMain.serverRoleManager.SetPlayerData(playerId, pos, dir);
        
        messageToSend.AddUShort(playerId);
        messageToSend.AddVector3(pos);
        messageToSend.AddVector3(dir);
        Game.ServerNet.SendToAll(messageToSend);
    }
}