using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private bool canRotate = true;
    private bool isReturning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetUp(Vector2 vector2, float gravityScale)
    {
        rb.velocity = vector2;
        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.Instance.player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) < 1)
                PlayerManager.Instance.player.CatchTheSword();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }

    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }
}
