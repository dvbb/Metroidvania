using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();        
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.moveState);
    }

    public override void Exit()
    {
        base.Exit();

    }
}
