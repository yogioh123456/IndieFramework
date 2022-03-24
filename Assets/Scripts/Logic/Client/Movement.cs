using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement {
    private GameObject target;
    
    public Movement(GameObject go) {
        target = go;
        this.RegisterEvent();
    }

    [EventMsg]
    private void MoveInput(Vector3 vector3) {
        target.transform.Translate(vector3 * 0.05f);
    }
}
