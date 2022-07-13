using System;
using UnityEngine;

public class PlayerMono : MonoBehaviour {
    public CharacterController characterController;
    public Animator animator;
    public AnimationCurve jumpCurve;
    
    public Action updateEvent;
    private void Update()
    {
        updateEvent?.Invoke();
    }
}
