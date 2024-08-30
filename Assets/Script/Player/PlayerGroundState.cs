using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(player.BlackholeState);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasSword())
            stateMachine.ChangeState(player.AimSwordState);

        if (Input.GetKey(KeyCode.Mouse0))
            stateMachine.ChangeState(player.AttackState);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.AirState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
    }

    private bool HasSword()
    {
        if (player.sword == null)
            return true;
        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
