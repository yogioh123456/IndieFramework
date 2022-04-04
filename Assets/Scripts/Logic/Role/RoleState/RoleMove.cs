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
        roleStateManager.animator.CrossFade("run", 0.25f);
    }

    public void UpdateHandle()
    {
        Debug.Log("move");
    }

    public void Exit()
    {
        
    }
}
