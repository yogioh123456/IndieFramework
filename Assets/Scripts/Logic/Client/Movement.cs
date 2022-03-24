using System.Collections;
using System.Collections.Generic;
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
    }
}
