using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
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
        Debug.Log("skeleton.IsPlayerDetected().distance:  " + skeleton.IsPlayerDetected().distance);
        Debug.Log("distance:  " + skeleton.attackDistance);
        if (skeleton.IsPlayerDetected().distance < skeleton.attackDistance)
            stateMachine.ChangeState(skeleton.attackState);
        else
            stateMachine.ChangeState(skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();

    }
}
