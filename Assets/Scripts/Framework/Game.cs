using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Game : EntityStatic
{
    public static void Update() {
        for (int i = updateList.Count - 1; i >= 0; i--) {
            updateList[i].Update();
        }
    }
    
    public static void FixedUpdate() {
        for (int i = fixedUpdateList.Count - 1; i >= 0; i--) {
            fixedUpdateList[i].FixedUpdate();
        }
    }
    
    public static void OnApplicationQuit() {
        for (int i = applicationList.Count - 1; i >= 0; i--) {
            applicationList[i].OnApplicationQuit();
        }
    }
}
