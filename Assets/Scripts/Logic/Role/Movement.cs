using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class Movement : Entity {
    private Vector3 gravity = Vector3.down;
    private CharacterController characterController;
    private PlayerControl playerControl;
    public Movement(PlayerControl player) {
        playerControl = player;
        characterController = playerControl.playerMono.characterController;
    }

    public override void Dispose() {
        base.Dispose();
    }
    
    public void RoleMove(Vector3 vector3)
    {
        //target.transform.Translate(vector3 * 0.05f);
        if (!characterController.isGrounded) {
            vector3 += gravity;
        }
        characterController.Move(vector3 * Time.deltaTime * 4);
        SendMove();
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
