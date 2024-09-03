using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                EnemyStats target = collider.GetComponent<EnemyStats>();
                player.Stats.DoDamage(target);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }

    private void CatchSword()
    {
        player.StateMachine.ChangeState(player.IdleState);
    }
}
