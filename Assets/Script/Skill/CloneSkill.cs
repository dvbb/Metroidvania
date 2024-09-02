using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] public GameObject clonePrefab;
    [SerializeField] public float cloneDuration;
    [Space]
    [SerializeField] public bool canAttack;
    [SerializeField] public bool canAttackAtStart;
    [SerializeField] public bool canAttackAtEnd;

    public void CreateClone(Transform clonePosition, Vector3 offset)
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneDuration, canAttack, offset, FoundClosestEnemy(player.transform));
    }

    public void CreateCloneOnDashStart(Transform clonePosition, Vector3 offset)
    {
        if(canAttackAtStart) 
            CreateClone(clonePosition, offset);
    }
    public void CreateCloneOnDashOver(Transform clonePosition, Vector3 offset)
    {
        if (canAttackAtEnd)
            CreateClone(clonePosition, offset);
    }

}
