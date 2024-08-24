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
        if (skeleton.IsPlayerDetected())
        {
            stateTimer = skeleton.battleTime;
            if (skeleton.IsPlayerDetected().distance < skeleton.attackDistance && CanAttack())
                stateMachine.ChangeState(skeleton.attackState);
        }
        else
        {
            if (stateTimer < 0)
                stateMachine.ChangeState(skeleton.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

    }

    protected bool CanAttack()
    {
        if (Time.time > skeleton.lastTimeAttacked + skeleton.attackColdDown)
        {
            skeleton.lastTimeAttacked = Time.time;
            return true;
        }
        return false;

    }
}
