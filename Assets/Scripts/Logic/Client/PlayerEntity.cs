using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
    private Player player;
    private Movement movement;
    private GameObject playerObj;
    
    public PlayerEntity(Player player) {
        this.player = player;
        playerObj = Game.Asset.LoadAsset("Prefabs/hero");
        playerObj.SetZero();
        movement = new Movement(playerObj);
    }
}
