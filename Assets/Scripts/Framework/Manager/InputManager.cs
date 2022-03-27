using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Mono {
    public override void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            Game.Event.Dispatch("MoveInput", new Vector3(h, 0, v));
        }
    }
}
