﻿using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Skeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, StateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
