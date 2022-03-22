using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Game : EntityStatic
{
    public static void Init() {
        AddComp<MainLogic>();
        AddComp<UGUIManager>();
        AddComp<EventSystemManager>();
        AddComp<ServerNetwork>();
        AddComp<ClientNetwork>();
    }

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
    
    public static EventSystemManager Event => GetComp<EventSystemManager>();
    public static UGUIManager UI => GetComp<UGUIManager>();
}
