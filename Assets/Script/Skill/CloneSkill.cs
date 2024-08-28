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

    public void CreateClone(Transform clonePosition)
    {
        GameObject newClone = Instantiate(clonePrefab);

        Debug.Log(clonePosition.position.x);
        Debug.Log(clonePosition.position.y);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneDuration, canAttack);
    }
}
