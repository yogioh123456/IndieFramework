using System.Collections.Generic;
using UnityEngine;

public enum RoleState
{
    None,
    Idle,
    Move,
}

public class RoleStateManager : Entity, IUpdate
{
    public Animator animator;
    public Dictionary<RoleState,IRoleState> stateDic = new Dictionary<RoleState,IRoleState>();
    public RoleState curState;

    public RoleStateManager(Animator animator)
    {
        this.animator = animator;
        stateDic.Add(RoleState.Idle, new RoleIdle(this));
        stateDic.Add(RoleState.Move, new RoleMove(this));
        SetState(RoleState.Idle);
    }

    public void SetState(RoleState roleState)
    {
        if (curState == roleState)
        {
            return;
        }

        if (curState != RoleState.None)
        {
            stateDic[curState].Exit();
        }
        curState = roleState;
        stateDic[curState].Enter();
    }
    
    public void Update()
    {
        stateDic[curState].UpdateHandle();
    }
}
