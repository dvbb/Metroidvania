using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetUp(Vector2 vector2,float gravityScale)
    {
        rb.velocity = vector2;
        rb.gravityScale = gravityScale;
    }
}
