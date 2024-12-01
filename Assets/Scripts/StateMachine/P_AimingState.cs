using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AimingState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        //disable all movement
        //disable all movement controls
        //player.transform.rotation = player.groundedCameraRotation;
        player.rb.isKinematic = true;
        player.laser.SetActive(true);
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

        // Rotation (Mouse movement rotates the camera, not the player)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        player.mainCamera.Rotate(pitch, yaw, 0f, Space.Self);


        float angle = Vector3.Angle(player.transform.forward, player.mainCamera.forward);
        if (angle < 80f)
        {
            player.SwitchState(player.groundedState);
            return;
        }
        else if (angle > 210f)
        {
            // TODO: Limit rotation
        }

        // Draw the ray
        Vector3 offset = player.transform.up * 0.01f + player.transform.right * 0.01f; 
        Ray ray = new Ray(player.transform.position + offset, player.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (!Input.GetMouseButton(0))
        {
            player.SwitchState(player.flyingState);
            return;
        }

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            player.SwitchState(player.pushingState);
            return;
        }
    }

    public override void ExitState(P_StateManager player) 
    { 
        player.rb.isKinematic = false;
        //player.groundedCameraRotation = player.transform.rotation;
        player.laser.SetActive(false);
    }

}
