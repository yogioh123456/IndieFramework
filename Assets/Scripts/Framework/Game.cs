using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Game : EntityStatic
{
    public static void Update() {
        for (int i = monoList.Count - 1; i >= 0; i--) {
            monoList[i].Update();
        }
    }
    
    public static void FixedUpdate() {
        for (int i = monoList.Count - 1; i >= 0; i--) {
            monoList[i].FixedUpdate();
        }
    }
    
    public static void OnApplicationQuit() {
        for (int i = monoList.Count - 1; i >= 0; i--) {
            monoList[i].OnApplicationQuit();
        }
    }
}
