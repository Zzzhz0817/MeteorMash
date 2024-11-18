using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GrabbingState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        //disable all movement controls
    }


    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        
        if(PlayerControl.releases left mouse button)
        {
            player.SwitchState(player.flyingState);
        }
        if(PlayerControl.hits an object to attach to)
        {
            player.SwitchState(player.groundedState);
        }
        */

    }
}
