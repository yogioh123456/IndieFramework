using UnityEngine;

/// <summary>
/// 自由落体状态，没啥用了，只有配合Impulse
/// </summary>
public class RoleFall : IRoleState {
    private Vector2 fallDir;
    
    private RoleStateManager roleStateManager;
    public RoleFall(RoleStateManager roleStateManager)
    {
        this.roleStateManager = roleStateManager;
    }
    
    public void Enter() {
        Debug.Log("自由落体");
        this.RegisterEvent();
    }

    public void UpdateHandle() {
        if (roleStateManager.playerControl.IsGround) {
            roleStateManager.SetState(RoleState.Idle);
        }
        roleStateManager.playerControl.movement.RoleMove(new Vector3(fallDir.x, 0, fallDir.y));
    }

    public void Exit() {
        this.UnregisterEvent();
    }

    [EventMsg]
    public void SetFallDir(Vector2 v2) {
        fallDir = v2;
    }
}
