public class P_PushingState : P_State
{

    public override void EnterState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", true);
    }



    public override void UpdateState(P_StateManager player)
    {
        /*TO DO:         
        while both mouse buttons are held down
        gauge fills and empties for power management


        if(PlayerControl.releases right mouse button)
        {
            player.SwitchState(player.grabbingState);
        }
        if(playerControl.releases left mouse button)
        {
            player.SwitchState(player.pushingState);
        }
        */
    }


public override void ExitState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", false);
    }

}
