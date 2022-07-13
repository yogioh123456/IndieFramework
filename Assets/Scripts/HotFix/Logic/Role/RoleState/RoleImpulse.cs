using UnityEngine;

/// <summary>
/// 冲击力，感觉用处不大，可再实现，根据跳跃
/// </summary>
public class RoleImpulse : IRoleState {
    //冲击力相关
    private Vector3 impulseDir;
    private float impulsePower;
    private float impulseTime;
    private float impulseTimeAll;
    private AnimationCurve impulseCurve;
    private float impulseCurveValue;
    
    private RoleStateManager roleStateManager;
    public RoleImpulse(RoleStateManager roleStateManager)
    {
        this.roleStateManager = roleStateManager;
    }
    
    public void Enter() {
        this.RegisterEvent();
    }

    public void UpdateHandle() {
        if (impulseTime < impulseTimeAll) {
            impulseCurveValue = impulseCurve.Evaluate(Mathf.Clamp01(impulseTime / impulseTimeAll));
            impulseTime += Time.deltaTime;
            roleStateManager.playerControl.movement.RoleMove(impulseDir * impulsePower * impulseCurveValue);
        } else {
            //冲击力消失进入自由落体状态
            roleStateManager.SetState(RoleState.Fall);
            Game.Event.Dispatch("SetFallDir", new Vector2(impulseDir.x, impulseDir.z));
        }
    }

    public void Exit() {
        Debug.Log("冲击力结束");
        this.UnregisterEvent();
    }
    
    [EventMsg]
    public void RoleAddForce(Vector3 dir, float power, float time, AnimationCurve animationCurve) {
        impulseDir = dir;
        impulsePower = power;
        impulseTimeAll = time;
        impulseTime = 0;
        impulseCurve = animationCurve;
    }
}
