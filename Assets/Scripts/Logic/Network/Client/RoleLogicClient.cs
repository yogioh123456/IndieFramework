using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class RoleLogicClient {
    //客户端接收消息
    [MessageHandler((ushort)Msg.Chat)]
    private static void PlayerChat(Message message)
    {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("客户端聊天" + playerId + str);
    }
    
    [MessageHandler((ushort)Msg.CreatePlayer)]
    private static void PlayerCreate(Message message)
    {
        ushort playerId = message.GetUShort();
        Debug.Log("客户端创建玩家" + playerId + "  " + Game.ClientNet.ID);
        /*
        if (playerId == Game.ClientNet.ID) {
            PlayerControl playerControl = Game.GetComp<PlayerManager>().AddPlayer(playerId);
            playerControl.AddComp<InputListener>(playerControl);//本地玩家加入输入监听
        } else {
            Game.GetComp<PlayerManager>().AddPlayer(playerId);
        }
        */
        Game.GetComp<PlayerManager>().AddPlayer(playerId);
    }
    
    [MessageHandler((ushort)Msg.Connected)]
    private static void PlayerConnected(Message message)
    {
        Debug.Log("连接服务器成功");
        Game.ClientNet.Send(Msg.CreatePlayer);
    }
    
    [MessageHandler((ushort)Msg.PlayerMove)]
    private static void PlayerMove(Message message)
    {
        ushort id = message.GetUShort();
        Vector3 pos = message.GetVector3();
        Vector3 dir = message.GetVector3();
        Game.GetComp<PlayerManager>().SetPlayerPos(id, pos, dir);
    }
    
    [MessageHandler((ushort)Msg.RemovePlayer)]
    private static void PlayerRemove(Message message)
    {
        ushort id = message.GetUShort();
        Game.GetComp<PlayerManager>().RemovePlayer(id);
    }
    
    // 恢复其他玩家的数据
    [MessageHandler((ushort)Msg.ServerRoleData)]
    private static void RoleDataRecovery(Message message)
    {
        var id = message.GetUShort();
        Debug.LogError(id);
        var data = message.GetBytes(true);
        var list = (List<PlayerNetData>) data.Bytes2Object();

        foreach (var one in list)
        {
            if (one.id != Game.ClientNet.ID) {
                Game.GetComp<PlayerManager>().AddOtherPlayer(one);
            }
        }
    }
    
    [MessageHandler((ushort)Msg.RoleState)]
    private static void RoleState(Message message)
    {
        ushort id = message.GetUShort();
        RoleState roleState = (RoleState)message.GetByte();
        Game.GetComp<PlayerManager>().SetPlayerState(id, roleState);
    }
}
