using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_SkeletonAnimTrigger : MonoBehaviour
{
    private Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        skeleton.AnimationFinishTrigger();
    }

    public void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.attackCheck.position,skeleton.attackCheckRadius);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
                collider.GetComponent<Player>().Damage();
        }
    }
}
