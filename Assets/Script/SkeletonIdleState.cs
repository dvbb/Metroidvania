using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonIdleState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.moveState);
    }
}
