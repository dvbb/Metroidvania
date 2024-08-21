﻿using System.Collections;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;
        Debug.Log("wall jump");
        Debug.Log(player.facingDir);
        player.SetVelocity(5 * player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.AirState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }

}
