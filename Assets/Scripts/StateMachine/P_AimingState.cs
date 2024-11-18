using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AimingState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        //disable all movement
        //disable all movement controls
    }


    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        Mouse movement = pitch/yaw
        start firing rays

        if(PlayerControl.releases left mouse button == true)
        {
            player.SwitchState(player.flyingState);

        }
        if(PlayerControl.holds both mouse buttons == true)
        {
            player.SwitchState(player.pushingState);
        }
        if(PlayerControl.moves camera back to <90 degree angle from center of object == true)
        {
            player.SwitchState(player.groundedState);

        }
        */
    }
}
