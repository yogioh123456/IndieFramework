using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl {
    public PlayerNetData player;
    public GameObject playerObj;
    public Animator animator;
    public PlayerMono playerMono;
    public RoleStateManager roleStateManager;
    public Movement movement;
    public bool IsGround => playerMono.characterController.isGrounded;

    public PlayerControl(PlayerNetData player) {
        this.player = player;
        playerObj = AssetManager.LoadAsset("Prefabs/hero");
        playerMono = playerObj.GetComponent<PlayerMono>();
        animator = playerMono.animator;
        movement = new Movement(this);
        roleStateManager = new RoleStateManager(this);
        playerMono.updateEvent += roleStateManager.Update;
        playerMono.updateEvent += movement.Update;
        playerObj.transform.position = player.pos;
        playerObj.transform.forward = player.dir;
    }
}
