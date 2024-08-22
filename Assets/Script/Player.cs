using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isBusy { get; private set; }

    [Header("Attack movement")]
    public Vector2[] attackMovement;

    [Header("Move info")]
    public float speed = 8f;
    public float jumpForce = 12;

    [Header("Dash info")]
    public float dashSpeed = 16f;
    public float dashDuration = 0.2f;
    public float dashDir = 1;
    public float dashUsageTimer = 0;
    public float dashColdDown = 4f;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

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
    #endregion


    private void Awake()
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
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        CheckDashInput();
    }

    public IEnumerable BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void CheckDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashColdDown;

            dashDir = Input.GetAxis("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            StateMachine.ChangeState(DashState);
        }
    }

    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public void ZeroVelocity() => SetVelocity(0, 0);
    #endregion

    #region Collision
    public void AnimationTrigger() => StateMachine.currentState.AnimatorFinishTrigger();
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(
                groundCheck.position.x,
                groundCheck.position.y - groundCheckDistance
            ));
        Gizmos.DrawLine(wallCheck.position, new Vector3(
                wallCheck.position.x + wallCheckDistance,
                wallCheck.position.y
            ));
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
            Flip();
        else if (xVelocity < 0 && facingRight)
            Flip();
    }
    #endregion
}
