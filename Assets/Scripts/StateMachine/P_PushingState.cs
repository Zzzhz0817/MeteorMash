using UnityEngine;

public class P_PushingState : P_State
{
    private float power = 0f;
    private float maxPower = 100f;

    public override void EnterState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", true);
        power = 0f;
        player.SetPower(power);
        player.UpdatePowerUI();
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
        Debug.Log(power);
        
        player.SetPower(power);
        player.UpdatePowerUI();



        float powerSpeed = 30f;
        if (power < maxPower)
        {
            power += powerSpeed * Time.deltaTime;
        }


        if (!Input.GetMouseButton(1))
        {
            player.SwitchState(player.previousState);
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            Vector3 pushDirection;
            pushDirection = player.transform.forward;

            player.rb.AddForce(pushDirection * power * 0.05f, ForceMode.Impulse);
            player.SwitchState(player.flyingState);
            return;
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.anim.SetBool("Push-Away", false);
        power = 0f;
        player.SetPower(power);
        player.UpdatePowerUI();
    }
}
