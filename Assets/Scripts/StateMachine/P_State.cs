
using UnityEngine;

public abstract class P_State
{
    public abstract void EnterState(P_StateManager player);

    public abstract void UpdateState(P_StateManager player);

}