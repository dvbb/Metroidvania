using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        SkillManager.instance.clone.CreateCloneOnDashStart(player.transform, new Vector3(0, 0, 0));

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
        SkillManager.instance.clone.CreateCloneOnDashOver(player.transform, new Vector3(0, 0, 0));
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashDir * player.dashSpeed, rb.velocity.y);

        if (!player.IsGroundDetected() && player.IsWallDetected()) 
            stateMachine.ChangeState(player.WallSlideState);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);
    }
}
