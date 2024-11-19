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
        // Aiming in half speed
        float mouseX = Input.GetAxis("Mouse X") * 0.5f;
        float mouseY = Input.GetAxis("Mouse Y") * 0.5f;
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        player.rb.transform.Rotate(pitch, yaw, 0f, Space.Self);

        if (!Input.GetMouseButton(0))
        {
            player.SwitchState(player.flyingState);
        }
    }

    public override void OnCollisionEnter(P_StateManager player, Collision collision)
    {
        if (Input.GetMouseButton(0))
        {
            player.groundedObject = collision.transform;
            player.SwitchState(player.groundedState);
        }
    }
}
