using System.Collections.Generic;
using UnityEngine;

public enum RoleState
{
    None,
    Idle,
    Move,
    Pose,
}

public class RoleStateManager : Entity, IUpdate
{
    public Dictionary<RoleState,IRoleState> stateDic = new Dictionary<RoleState,IRoleState>();
    public RoleState curState;
    public PlayerControl playerControl;

    public RoleStateManager(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
        stateDic.Add(RoleState.Idle, new RoleIdle(this));
        stateDic.Add(RoleState.Move, new RoleMove(this));
        stateDic.Add(RoleState.Pose, new RolePose(this));
        SetState(RoleState.Idle);
    }

    public bool IsLocalPlayer()
    {
        return playerControl.player.id == Game.ClientNet.ID;
    }

    public void PlayAnim(string name)
    {
        playerControl.animator.CrossFade(name, 0.25f);
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
        SendRoleState();
    }

    public void SendRoleState()
    {
        if (IsLocalPlayer())
        {
            Game.ClientNet.Send(Msg.RoleState, (byte)curState);
        }
    }
    
    public void Update()
    {
        stateDic[curState].UpdateHandle();
    }
}
