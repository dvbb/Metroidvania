using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;

    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer <= 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("Skill cooldown");
        return false;
    }

    public virtual void UseSkill()
    {
    }

    protected virtual Transform FoundClosestEnemy(Transform startTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(startTransform.position, 25);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var item in colliders)
        {
            if (item.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(startTransform.position, item.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = item.transform;
                }
            }
        }
        Debug.Log("closestEnemy:" + closestEnemy);

        return closestEnemy;
    }
}
