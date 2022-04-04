using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class ServerRoleManager : Entity,IUpdate
{
    private Dictionary<ushort, PlayerNetData> playerDic = new Dictionary<ushort, PlayerNetData>();

    public ServerRoleManager()
    {
        this.RegisterEvent();
    }
    
    public override void Dispose() {
        this.UnregisterEvent();
        base.Dispose();
    }
    
    // 重连下发数据
    public void Reconnected(ushort id)
    {
        Message message = Message.Create(MessageSendMode.reliable, Msg.ServerRoleData);
        List<PlayerNetData> list = new List<PlayerNetData>();
        foreach (var one in playerDic)
        {
            list.Add(one.Value);
        }

        message.AddUShort(id);
        message.AddBytes(list.Object2Bytes(), true, true);
        Game.ServerNet.Send(message, id);
    }
    
    public void AddPlayer(ushort id, PlayerNetData player)
    {
        playerDic.Add(id, player);
    }
    
    public void SetPlayerData(ushort id, Vector3 pos, Vector3 dir)
    {
        if (!playerDic.ContainsKey(id))
        {
            Debug.LogError("玩家不存在" + id);
            return;
        }
        playerDic[id].pos = pos;
        playerDic[id].dir = dir.normalized;
    }

    public void RemovePlayer(ushort id)
    {
        playerDic.Remove(id);
    }

    [EventMsg]
    public void ClientDisconnected(ushort id)
    {
        if (playerDic.ContainsKey(id))
        {
            Debug.Log($"玩家{id}掉线");
            playerDic.Remove(id);
            //告诉其他玩家有人掉线了
            Message message = Message.Create(MessageSendMode.reliable,Msg.RemovePlayer);
            message.AddUShort(id);
            Game.ServerNet.SendToAll(message);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            List<PlayerNetData> list = new List<PlayerNetData>();
            foreach (var one in playerDic)
            {
                list.Add(one.Value);
            }

            var a = list.Object2Bytes();
            var b = a.Bytes2Object();
            Debug.Log(b);
        }
    }
}
