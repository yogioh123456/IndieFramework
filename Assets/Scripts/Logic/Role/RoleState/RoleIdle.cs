using UnityEngine;

public class RoleIdle : IRoleState
{
    private RoleStateManager roleStateManager;

    public RoleIdle(RoleStateManager roleStateManager)
    {
        this.roleStateManager = roleStateManager;
    }
    
    public void Enter()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.RegisterEvent();
        }
        roleStateManager.PlayAnim("idle");
    }

    public void UpdateHandle()
    {
        
    }

    public void Exit()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.UnregisterEvent();
        }
    }

    [EventMsg]
    private void Pose()
    {
        roleStateManager.SetState(RoleState.Pose);
    }
    
    [EventMsg]
    private void MoveInput(Vector3 vector3)
    {
        float h = vector3.x;
        float v = vector3.z;
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            roleStateManager.SetState(RoleState.Move);
        }
    }
    
    [EventMsg]
    private void Jump()
    {
        roleStateManager.playerControl.movement.Jump();
    }
}
