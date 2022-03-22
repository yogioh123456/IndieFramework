using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUIView : MonoBehaviour
{
    public Action updateEvent;
    
    private void Update()
    {
        updateEvent?.Invoke();
    }
}
