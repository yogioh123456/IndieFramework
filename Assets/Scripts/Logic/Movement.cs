﻿using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

public class Movement : Entity {
    private GameObject target;
    
    public Movement(GameObject player) {
        target = player;
        this.RegisterEvent();
    }

    public override void Dispose() {
        this.UnregisterEvent();
        base.Dispose();
    }

    [EventMsg]
    private void MoveInput(Vector3 vector3) {
        target.transform.Translate(vector3 * 0.05f);
        SendMove();
    }

    private void SendMove()
    {
        //发送不可靠的消息
        Message message = Message.Create(MessageSendMode.unreliable, Msg.playerMove, shouldAutoRelay: true);
        message.AddUShort(Game.ClientNet.ID);
        message.AddVector3(target.transform.position);
        message.AddVector3(target.transform.forward);
        Game.ClientNet.Send(message);
    }
}