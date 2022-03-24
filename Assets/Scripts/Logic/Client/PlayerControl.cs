using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Entity {
    private Player player;
    public GameObject playerObj;
    
    public PlayerControl(Player player) {
        this.player = player;
        playerObj = Game.Asset.LoadAsset("Prefabs/hero");
        playerObj.SetZero();
    }
}
