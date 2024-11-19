using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_DialogueState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        // disable all movement
        // disable all movement controls
    }

    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        
        if(PlayerControl.presses f == true || mouse clicks)
        {
            progresses dialogue or chooses dialogue option
            returns to previousState
        }
        */
        if (Input.GetKeyDown(KeyCode.F))
        {
            // ialogue logic
            player.SwitchState(player.previousState);
        }
    }
}
