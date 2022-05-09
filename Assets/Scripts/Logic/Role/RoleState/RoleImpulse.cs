using UnityEngine;

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
        } else {
            //TODO:退出冲击力状态，冲击力状态最好是叠加状态
            roleStateManager.SetState(RoleState.Idle);
        }
        roleStateManager.playerControl.movement.RoleMove(impulseDir * impulsePower * impulseCurveValue);
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
