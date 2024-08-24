using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonStunnedState : SkeletonGroundState
{
    public SkeletonStunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1;
        skeleton.fx.InvokeRepeating("RedColorBlink",0,.1f);
        skeleton.rb.velocity = new Vector2(skeleton.stunDirection.x * skeleton.facingDir, skeleton.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancleRedBlink", 0);


    }
}
