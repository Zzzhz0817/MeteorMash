using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FlyingState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        //enable flying movement
    }


    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        WASD = up down left right movement
        QE = roll left right
        Mouse movement = pitch/yaw
        Shift = speed up
        Space = brake
        
        if(PlayerControl.isGrabbing ==true)
        {
            player.SwitchState(player.grabbingState);
        }
       / */
    }
}
