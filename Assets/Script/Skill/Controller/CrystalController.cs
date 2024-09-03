using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    #region Components
    private SpriteRenderer sr;
    private Animator animator => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    #endregion

    private float crystalExistTimer;
    private bool canExplore;
    private bool canMove;
    private float moveSpeed;
    private Transform closestEnemy;

    public void SetupCrystal(float existDuration, bool canExplore, bool canMove, float moveSpeed, Transform closestEnemy)
    {
        crystalExistTimer = existDuration;
        this.canExplore = canExplore;
        this.canMove = canMove;
        this.moveSpeed = moveSpeed;
        this.closestEnemy = closestEnemy;
    }

    private void Update()
    {
        crystalExistTimer -= Time.deltaTime;
        if (crystalExistTimer < 0)
            FinishCrystal();
        if (canMove && closestEnemy!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, moveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, closestEnemy.position) < 1)
            {
                canMove = false;
                FinishCrystal();
            }
        }
    }

    public void FinishCrystal()
    {
        if (canExplore)
            animator.SetBool("explode", true);
        else
            SelfDestroy();
    }

    private void AnimationExlodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                EnemyStats target = collider.GetComponent<EnemyStats>();
                PlayerManager.Instance.player.Stats.DoDamage(target);
            }
        }
        animator.SetBool("explode", false);
        SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);
}
