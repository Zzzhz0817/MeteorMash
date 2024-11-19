
using UnityEngine;

public abstract class P_State
{
    public abstract void EnterState(P_StateManager player);

    public abstract void UpdateState(P_StateManager player);

    public virtual void ExitState(P_StateManager player) { }
    
    public virtual void OnCollisionEnter(P_StateManager player, Collision collision) { }

}