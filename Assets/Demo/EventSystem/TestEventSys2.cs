using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventSys2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.RegisterEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [EventMsg]
    public void TestCode()
    {
        Debug.Log("TestCode s");
    }
}
