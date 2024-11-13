using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected PlayerController player;
    protected Animator animator;

    protected BaseState(PlayerController player, Animator animator)
    {

        this.player = player; 
        this.animator = animator;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
