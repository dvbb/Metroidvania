using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = .4f;
    private bool skillUsed;
    private float defaultGravity;
    public PlayerBlackholeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimatorFinishTrigger()
    {
        base.AnimatorFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        skillUsed = false;
        stateTimer = flyTime;
        defaultGravity = player.rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravity;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);
        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);
            if (!skillUsed)
            {
                SkillManager.instance.blackhole.UseSkill();
                skillUsed = true;
            }
        }
        if(SkillManager.instance.blackhole.CheckBlackholeFinished())
            stateMachine.ChangeState(player.AirState);
    }
}
