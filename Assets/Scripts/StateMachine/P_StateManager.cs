using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_StateManager : MonoBehaviour
{
    public P_State currentState;
    public P_State previousState;

    public P_FlyingState flyingState = new P_FlyingState();
    public P_GroundedState groundedState = new P_GroundedState();
    public P_GrabbingState grabbingState = new P_GrabbingState();
    public P_AimingState aimingState = new P_AimingState();
    public P_PushingState pushingState = new P_PushingState();
    public P_DialogueState dialogueState = new P_DialogueState();
    public P_PausedState pausedState = new P_PausedState();


    private void Start()
    {
        currentState = flyingState;

        currentState.EnterState(this);

    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(P_State state)
    {
        previousState = currentState;
        currentState = state;
        state.EnterState(this);
    }
}
