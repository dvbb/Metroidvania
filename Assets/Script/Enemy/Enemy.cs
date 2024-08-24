using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    public float battleTime;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Attack info")]
    public float attackDistance;
    public float attackColdDown;
    public float lastTimeAttacked;

    public EnemyStateMachine StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTrigger() => StateMachine.currentState.AnimatorFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, attackDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(
                transform.position.x + attackDistance * facingDir,
                transform.position.y,
                0
            ));
    }
}
