using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public void AddPlayer() {
        Player player = new Player();
        PlayerControl playerControl = new PlayerControl(player);
        playerControl.AddComp<Movement>(playerControl.playerObj);
    }
    
    public void AddOtherPlayer() {
        Player player = new Player();
        PlayerControl playerControl = new PlayerControl(player);
    }
}

public class Player {
    public string id;
    public string name;
}