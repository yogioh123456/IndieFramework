using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class Movement {
    private bool isGravity = true;//开启重力
    private float JumpHeight = 1.2f;//跳跃高度
    private float Gravity = -15.0f;//重力大小
    private Vector3 velocity;//当前速度
    private float maxFallSpeed = -20;//最大下落速度
    private CharacterController characterController;
    private PlayerControl playerControl;
    public Movement(PlayerControl player) {
        playerControl = player;
        characterController = playerControl.playerMono.characterController;
    }

    public void RoleMove(Vector3 vector3)
    {
        characterController.Move(vector3 * Time.deltaTime * 4);
        SendMove();
    }

    public void Jump() {
        velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
    }
    
    public void Update() {
        if (isGravity) {
            velocity.y += Gravity * Time.deltaTime;
        }
        if (velocity.y < maxFallSpeed) {
            velocity.y = maxFallSpeed;
        }
        playerControl.playerMono.characterController.Move(velocity * Time.deltaTime);
    }
    
    private void SendMove()
    {
        //发送不可靠的消息
        Message message = Message.Create(MessageSendMode.unreliable, Msg.PlayerMove, shouldAutoRelay: true);
        message.AddUShort(Game.ClientNet.ID);
        message.AddVector3(playerControl.playerMono.transform.position);
        message.AddVector3(playerControl.playerMono.transform.forward);
        Game.ClientNet.Send(message);
    }
}
