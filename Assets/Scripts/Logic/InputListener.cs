
using UnityEngine;

public class InputListener : Entity
{
    private PlayerControl playerControl;
    
    public InputListener(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
        this.RegisterEvent();
    }
    
    public override void Dispose() {
        this.UnregisterEvent();
        base.Dispose();
    }
    
    [EventMsg]
    private void MoveInput(Vector3 vector3)
    {
        float h = vector3.x;
        float v = vector3.z;
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            playerControl.GetComp<Movement>().Move(vector3);
            playerControl.GetComp<RoleStateManager>().SetState(RoleState.Move);
        }
        else
        {
            playerControl.GetComp<RoleStateManager>().SetState(RoleState.Idle);
        }
    }
}
