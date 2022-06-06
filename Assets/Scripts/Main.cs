using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;

public class Main : MonoBehaviour
{
    void Start() {
        CodeLoader.Instance.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) {
            CodeLoader.Instance.Run();
        }
        //CodeLoader.Instance.Update();
    }

    void FixedUpdate()
    {
        //CodeLoader.Instance.FixedUpdate();
    }
    
    void LateUpdate()
    {
        //CodeLoader.Instance.LateUpdate();
    }

    void OnApplicationQuit() {
        CodeLoader.Instance.OnApplicationQuit();
        CodeLoader.Instance.Dispose();
    }
}
