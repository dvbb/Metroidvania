using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("skill info")]
    [SerializeField] private float existTime = 4;
    [SerializeField] private bool canExplore;
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            CrystalController controller = currentCrystal.GetComponent<CrystalController>();
            controller.SetupCrystal(existTime,canExplore,canMove,moveSpeed, FoundClosestEnemy(player.transform));
        }
        else
        {
            Vector3 temp = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = temp;
            currentCrystal.GetComponent<CrystalController>()?.FinishCrystal();
        }
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
