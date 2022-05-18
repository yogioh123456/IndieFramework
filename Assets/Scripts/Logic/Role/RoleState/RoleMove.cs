using UnityEngine;

public class RoleMove : IRoleState {
    private Vector2 moveDir;
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
        moveDir = new Vector2(h, v);
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            roleStateManager.playerControl.movement.RoleMove(vector3);
        }
        else
        {
            roleStateManager.SetState(RoleState.Idle);
        }
    }
    
    [EventMsg]
    private void Jump()
    {
        roleStateManager.playerControl.movement.Jump();
    }
}
