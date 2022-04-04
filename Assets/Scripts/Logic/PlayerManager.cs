using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private Dictionary<ushort, PlayerControl> playerControlDic = new Dictionary<ushort, PlayerControl>();
    
    public void AddPlayer(ushort id) {
        PlayerNetData player = new PlayerNetData();
        PlayerControl playerControl = new PlayerControl(player);
        playerControl.AddComp<Movement>(playerControl.playerObj);
        playerControlDic.Add(id, playerControl);
    }
    
    public void AddOtherPlayer(ushort id) {
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
            transform.position = pos;
            transform.forward = forward.normalized;
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