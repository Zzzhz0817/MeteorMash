using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{ 
    protected PlayerStateMachine stateMachine;
    protected PlayerControl player;
    private string animBoolName;
    protected Rigidbody rb;



    public PlayerState(PlayerControl _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }
}
