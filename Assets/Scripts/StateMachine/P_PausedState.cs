using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_PausedState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        // disable all movement
        // disable all movement controls
        // save previous state
    }


    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        
        if(PlayerControl.presses ESC == true || mouse clicks)
        {
            progresses unpauses game or selects options including exit game.
            unpauses returns to previous state and switches that to currentstate
        }
        */
    }
}
