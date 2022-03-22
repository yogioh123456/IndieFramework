using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Game.Init();
        Game.GetComp<MainLogic>().Init();
    }
    
    void Update()
    {
        Game.Update();
    }

    void FixedUpdate()
    {
        Game.FixedUpdate();
    }

    void OnApplicationQuit() {
        Game.OnApplicationQuit();
    }
}
