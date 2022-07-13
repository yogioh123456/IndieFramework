using UnityEngine;

public class RolePose : IRoleState
{
    private float time;
    private float poseTime = 4.3f;
    private RoleStateManager roleStateManager;
    public RolePose(RoleStateManager roleStateManager)
    {
        this.roleStateManager = roleStateManager;
    }
    
    public void Enter()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.RegisterEvent();
        }
        time = 0;
        roleStateManager.PlayAnim("pose");
    }

    public void UpdateHandle()
    {
        time += Time.deltaTime;
        if (time > poseTime)
        {
            roleStateManager.SetState(RoleState.Idle);
        }
    }

    public void Exit()
    {
        if (roleStateManager.IsLocalPlayer())
        {
            this.UnregisterEvent();
        }
    }
}
