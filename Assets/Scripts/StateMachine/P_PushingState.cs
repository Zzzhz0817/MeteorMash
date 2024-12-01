using UnityEngine;

public class P_PushingState : P_State
{
    private float power = 0f;
    private float maxPower = 100f;
    private float powerChangeSpeed = 1f;
    private float timeElapsed = 0f;

    public override void EnterState(P_StateManager player)
    {
        player.powerSlideBar.gameObject.SetActive(true);
        player.anim.SetBool("Push-Away", true);
        power = 0f;
        timeElapsed = 0f;
        player.SetPower(power);
        player.UpdatePowerUI();
        player.laser.SetActive(true);
    }

    public override void UpdateState(P_StateManager player)
    {
        // Circular power change logic
        timeElapsed += Time.deltaTime * powerChangeSpeed;
        
        // Use a sine function to oscillate power between 0 and maxPower
        power = (Mathf.Sin(timeElapsed) + 1) / 2 * maxPower;

        // Adjust speed dynamically: faster near 100
        float closenessFactor = Mathf.Abs(maxPower - power) / maxPower; // 0 near 100, 1 near 0
        powerChangeSpeed = Mathf.Lerp(1f, 10f, 1 - closenessFactor); // Invert closenessFactor for faster near 100

        player.SetPower(power);
        player.UpdatePowerUI();

        if (!Input.GetMouseButton(1))
        {
            player.SwitchState(player.previousState);
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            Vector3 pushDirection = player.mainCamera.forward;
            player.rb.AddForce(pushDirection * power * 0.02f, ForceMode.Impulse);
            player.transform.rotation = player.mainCamera.rotation;
            player.SwitchState(player.flyingState);
            return;
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.powerSlideBar.gameObject.SetActive(false);
        player.anim.SetBool("Push-Away", false);
        power = 0f;
        player.SetPower(power);
        player.UpdatePowerUI();
        player.laser.SetActive(false);
    }
}
