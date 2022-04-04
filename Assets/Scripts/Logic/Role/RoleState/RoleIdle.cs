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
        roleStateManager.animator.CrossFade("idle", 0.25f);
    }

    public void UpdateHandle()
    {
        Debug.Log("idle");
    }

    public void Exit()
    {
        
    }
}
