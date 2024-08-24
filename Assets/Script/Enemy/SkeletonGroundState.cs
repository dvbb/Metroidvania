using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonGroundState : EnemyState
{
    protected Enemy_Skeleton skeleton;
    public SkeletonGroundState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
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
        if (skeleton.IsPlayerDetected())
            stateMachine.ChangeState(skeleton.battleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
