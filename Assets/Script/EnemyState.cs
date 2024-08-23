using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    protected bool triggerCalled;
    public float stateTimer;

    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public void Enter()
    {
        triggerCalled = false;
        enemy.Anim.SetBool(animBoolName, true);
    }

    public void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }
}
