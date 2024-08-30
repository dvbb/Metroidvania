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

    public void CreateClone(Transform clonePosition, Vector3 offset )
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneDuration, canAttack, offset);
    }
}
