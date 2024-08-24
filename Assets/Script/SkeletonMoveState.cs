using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed * skeleton.facingDir, skeleton.rb.velocity.y);
        if (skeleton.IsWallDetected() || !skeleton.IsGroundDetected())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
