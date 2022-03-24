using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public void AddPlayer() {
        Player player = new Player();
        PlayerEntity playerEntity = new PlayerEntity(player);
    }
}

public class Player {
    public string id;
    public string name;
}