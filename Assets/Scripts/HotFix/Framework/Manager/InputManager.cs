using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[GameComp]
public class InputManager : IUpdate {
    public void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Game.Event.Dispatch("MoveInput", new Vector3(h, 0, v));

        if (Input.GetKeyDown(KeyCode.R))
        {
            Game.Event.Dispatch("Pose");
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Game.Event.Dispatch("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Comma)) {
            if (Game.UI.GetUI<UI_DevTool>() == null) {
                Game.UI.OpenUI<UI_DevTool>();
                return;
            }
            bool active = Game.UI.GetUI<UI_DevTool>().selfView.gameObject.activeSelf;
            if (active) {
                Game.UI.CloseUI<UI_DevTool>();
            } else {
                Game.UI.OpenUI<UI_DevTool>();
            }
        }
    }
}
