using System;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    public Animator animator;
    
    public Action updateEvent;
    private void Update()
    {
        updateEvent?.Invoke();
    }
}
