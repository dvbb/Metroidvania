using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    protected Animator Anim { get; private set; }
    protected Rigidbody2D rb { get; private set; }
    #endregion

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }

    #region Velocity
    protected virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    protected virtual void ZeroVelocity() => SetVelocity(0, 0);
    #endregion

    #region Collision
    protected virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    protected virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
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
    protected virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void FlipController(float xVelocity)
    {
        if (xVelocity > 0 && !facingRight)
            Flip();
        else if (xVelocity < 0 && facingRight)
            Flip();
    }
    #endregion
}
