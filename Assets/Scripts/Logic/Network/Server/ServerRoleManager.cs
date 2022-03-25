using System.Collections.Generic;
using UnityEngine;

public class ServerRoleManager : Entity
{
    private Dictionary<ushort, PlayerServerData> playerDic = new Dictionary<ushort, PlayerServerData>();

    public ServerRoleManager()
    {
        this.RegisterEvent();
    }
    
    public override void Dispose() {
        this.UnregisterEvent();
        base.Dispose();
    }
    
    public void AddPlayer(ushort id, PlayerServerData player)
    {
        playerDic.Add(id, player);
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
        }
    }
}
