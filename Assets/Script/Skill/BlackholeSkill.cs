using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skill
{
    public GameObject blackholePrefab;

    [Header("Circle Lerb info")]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    [Header("Attack info")]
    [SerializeField] private float amountOfAttacks;

    [SerializeField] private float duration;

    BlackHoleController currentBlackhole;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject blackhole = Instantiate(blackholePrefab, PlayerManager.Instance.player.transform.position, Quaternion.identity);
        currentBlackhole = blackhole.GetComponent<BlackHoleController>();
        currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, duration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool CheckBlackholeFinished()
    {
        if (currentBlackhole == null) 
            return false;
        if (currentBlackhole.playerCanExit)
        {
            currentBlackhole = null;
            return true;
        }
        return false;
    }
}
