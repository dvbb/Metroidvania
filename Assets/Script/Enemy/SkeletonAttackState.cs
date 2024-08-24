using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetZeroVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.lastTimeAttacked = Time.time;
    }
}
