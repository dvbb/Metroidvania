using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonDeadState : SkeletonGroundState
{
    public SkeletonDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();        
        skeleton.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
