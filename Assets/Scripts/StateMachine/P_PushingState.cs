using UnityEngine;

public class P_PushingState : P_State
{
    private float power = 0f;
    private bool increasing = true;

    public override void EnterState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", true);
        power = 0f;
        increasing = true;
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
         // Power bar logic

        float powerSpeed = 1f;
        power += (increasing ? 1 : -1) * powerSpeed * Time.deltaTime;
        if (power >= 1f)
        {
            power = 1f;
            increasing = false;
        }
        else if (power <= 0f)
        {
            power = 0f;
            increasing = true;
        }

        // State transitions
        if (!Input.GetMouseButton(1))
        {
            player.SwitchState(player.previousState);
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            // Perform push
            Vector3 pushDirection;

            pushDirection = player.transform.forward;

            player.rb.AddForce(pushDirection * power * player.acceleration, ForceMode.Impulse);
            player.SwitchState(player.flyingState);
            return;
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", false);
        power = 0f;
    }
}
