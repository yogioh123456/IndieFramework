using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Game : EntityStatic
{
    public static void Update() {
        for (int i = 0; i < updateList.Count; i++)
        {
            updateList[i].Update();
        }
    }
    
    public static void FixedUpdate() {
        for (int i = 0; i < fixedUpdateList.Count; i++)
        {
            fixedUpdateList[i].FixedUpdate();
        }
    }
    
    public static void OnApplicationQuit() {
        for (int i = 0; i < applicationList.Count; i++)
        {
            applicationList[i].OnApplicationQuit();
        }
    }
}
