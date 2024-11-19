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

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Transform groundedObject;

    // Movement variables
    public float moveSpeed = 10f;
    public float rotationSpeed = 500f;
    public float rollSpeed = 50f;
    public float acceleration = 50f;
    public float drag = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = flyingState;

        currentState.EnterState(this);

    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(P_State state)
    {
        currentState.ExitState(this);
        previousState = currentState;
        currentState = state;
        state.EnterState(this);
    }

    public void SwitchToPreviousState()
    {
        if (previousState != null)
        {
            SwitchState(previousState);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
