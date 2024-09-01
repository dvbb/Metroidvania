using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public bool isBusy { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackMovement;

    [Header("Move info")]
    public float speed = 8f;
    public float jumpForce = 12;

    [Header("Dash info")]
    public float dashSpeed = 16f;
    public float dashDuration = 0.2f;
    public float dashDir = 1;

    public GameObject sword { get; private set; }

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerPrimaryAttackState AttackState { get; private set; }
    public PlayerAimSwordState AimSwordState { get; private set; }
    public PlayerCatchSwordState CatchSwordState { get; private set; }
    public PlayerBlackholeState BlackholeState { get; private set; }
    #endregion


    protected override void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "WallJump");

        AttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");

        AimSwordState = new PlayerAimSwordState(this, StateMachine, "AimSword");
        CatchSwordState = new PlayerCatchSwordState(this, StateMachine, "CatchSword");

        BlackholeState = new PlayerBlackholeState(this, StateMachine, "Jump");
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        StateMachine.currentState.Update();
        CheckDashInput();

        if (Input.GetKeyDown(KeyCode.F))
            SkillManager.instance.crystal.UseSkill();

    }

    public IEnumerable BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void CheckDashInput()
    {

        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxis("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            StateMachine.ChangeState(DashState);
        }
    }
    public void AnimationTrigger() => StateMachine.currentState.AnimatorFinishTrigger();

    public void AssignNewSword(GameObject newSword) => sword = newSword;
    public void CatchTheSword()
    {
        StateMachine.ChangeState(CatchSwordState);
        Destroy(sword);
    }
    public void ExitBlackHoleAbility()
    {
        StateMachine.ChangeState(AirState);
    }
}
