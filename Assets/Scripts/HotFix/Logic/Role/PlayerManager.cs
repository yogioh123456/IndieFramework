using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[GameComp]
public class PlayerManager
{
    private Dictionary<ushort, PlayerControl> playerControlDic = new Dictionary<ushort, PlayerControl>();

    public void AddPlayer(ushort id) {
        PlayerNetData player = new PlayerNetData();
        player.id = id;
        PlayerControl playerControl = new PlayerControl(player);
        playerControlDic.Add(id, playerControl);
    }
    
    public void AddOtherPlayer(PlayerNetData player) {
        PlayerControl playerControl = new PlayerControl(player);
        playerControlDic.Add(player.id, playerControl);
    }

    public void SetPlayerPos(ushort id, Vector3 pos, Vector3 forward)
    {
        //不同步自己，只同步别人
        if (playerControlDic.ContainsKey(id) && id != Game.ClientNet.ID)
        {
            Transform transform = playerControlDic[id].playerObj.transform;
            transform.position = Vector3.Lerp(transform.position, pos, 2);
            transform.forward = forward.normalized;
        }
    }
    
    public void SetPlayerState(ushort id, RoleState state)
    {
        //不同步自己，只同步别人
        if (playerControlDic.ContainsKey(id) && id != Game.ClientNet.ID)
        {
            playerControlDic[id].roleStateManager.SetState(state);
        }
    }

    public void RemovePlayer(ushort id)
    {
        if (playerControlDic.ContainsKey(id))
        {
            var player = playerControlDic[id];
            Object.Destroy(player.playerObj);
            playerControlDic.Remove(id);
        }
    }
}

public class Player {
    public string id;
    public string name;
}