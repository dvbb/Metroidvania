using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdelState IdelState { get; private set; }
    public PlayerMoveState MoveState { get; set; }
    #endregion


    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdelState = new PlayerIdelState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();

        StateMachine.Initialize(IdelState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
    }
}
