using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Entity {
    private PlayerNetData player;
    public GameObject playerObj;
    
    public PlayerControl(PlayerNetData player) {
        this.player = player;
        playerObj = Game.Asset.LoadAsset("Prefabs/hero");
        Debug.LogError(player.pos);
        playerObj.transform.position = player.pos;
        playerObj.transform.forward = player.dir;
    }
}
