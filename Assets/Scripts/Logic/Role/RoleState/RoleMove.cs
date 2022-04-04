using UnityEngine;

public class RoleMove : IRoleState
{
    private RoleStateManager roleStateManager;

    public RoleMove(RoleStateManager roleStateManager)
    {
        this.roleStateManager = roleStateManager;
    }
    
    public void Enter()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.RegisterEvent();
        }
        roleStateManager.PlayAnim("run");
    }

    public void UpdateHandle()
    {
        //Debug.Log("move");
    }

    public void Exit()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.UnregisterEvent();
        }
    }
    
    [EventMsg]
    private void MoveInput(Vector3 vector3)
    {
        float h = vector3.x;
        float v = vector3.z;
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            roleStateManager.playerControl.movement.RoleMove(vector3);
        }
        else
        {
            roleStateManager.SetState(RoleState.Idle);
        }
    }
}
