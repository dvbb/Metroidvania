using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    private float cloneTimer;
    private float colorLosingSpeed = 1;
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackCheckRadius = 1f;

    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.speed = 4;
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
        if (sr.color.a < 0)
            Destroy(gameObject);
    }

    public void SetUpClone(Transform _newTransform, float _cloneDuration, bool canAttack, Vector3 offset)
    {
        if (canAttack)
            animator.SetInteger("AttackCount", Random.Range(1, 3));
        transform.position = _newTransform.position + offset;
        cloneTimer = _cloneDuration;
        FaceClostestTarget();
    }
    private void AnimationTrigger()
    {
        cloneTimer = -1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
                collider.GetComponent<Enemy>().Damage();
        }
    }

    private void FaceClostestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach (var item in colliders)
        {
            if (item.GetComponent<Enemy>()!=null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, item.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = item.transform;
                }
            }
        }

        if (closestEnemy != null && transform.position.x > closestEnemy.transform.position.x)
            transform.Rotate(0, 180, 0);
    }
}
