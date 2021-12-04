using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestEventSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.RegisterEvent();
        Game.Event.Dispatch("TestCode");
        Game.Event.Dispatch("TestCode2", 45);
        this.UnregisterEvent();
        Game.Event.Dispatch("TestCode");

        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic["sa"] = 566;
        dic["sa"] = 56644;
        Debug.Log(dic["sa"]);
        
        Game.Event.AddListener("fgsad",Test);
        Game.Event.Dispatch("fgsad");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Game.Event.Dispatch("TestCode");
        }
    }

    void Test()
    {
        Debug.Log("tttsss");
    } 
    
    [EventMsg]
    public void TestCode()
    {
        Debug.Log("TestCode x");
    }

    [EventMsg]
    public void TestCode2(int a)
    {
        Debug.Log("TestCode2   " + a);
    }
}
