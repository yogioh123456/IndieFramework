using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdate {
    public void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Game.Event.Dispatch("MoveInput", new Vector3(h, 0, v));

        if (Input.GetKeyDown(KeyCode.R))
        {
            Game.Event.Dispatch("Pose");
        }
    }
}
