﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private Dictionary<ushort, PlayerControl> playerControlDic = new Dictionary<ushort, PlayerControl>();
    
    public void AddPlayer(ushort id) {
        Player player = new Player();
        PlayerControl playerControl = new PlayerControl(player);
        playerControl.AddComp<Movement>(playerControl.playerObj);
        playerControlDic.Add(id, playerControl);
    }
    
    public void AddOtherPlayer(ushort id) {
        Player player = new Player();
        PlayerControl playerControl = new PlayerControl(player);
        playerControlDic.Add(id, playerControl);
    }

    public void SetPlayerPos(ushort id, Vector3 pos, Vector3 forward)
    {
        //TODO:真正的同步是不需要这一步的
        if (playerControlDic.ContainsKey(id))
        {
            Transform transform = playerControlDic[id].playerObj.transform; 
            transform.position = pos;
            transform.forward = forward.normalized;
        }
    }
}

public class Player {
    public string id;
    public string name;
}