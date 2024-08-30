using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skill
{
    public GameObject blackholePrefab;

    [Header("Circle Lerb info")]
    [SerializeField]private float maxSize;
    [SerializeField]private float growSpeed;
    [SerializeField]private float shrinkSpeed;
    
    [Header("Attack info")]
    [SerializeField] private float amountOfAttacks;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject blackhole = Instantiate(blackholePrefab);
        BlackHoleController blackHoleController = blackhole.GetComponent<BlackHoleController>();
        blackHoleController.SetupBlackhole(maxSize, growSpeed,shrinkSpeed,amountOfAttacks);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
