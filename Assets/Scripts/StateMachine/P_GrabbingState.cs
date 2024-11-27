using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GrabbingState : P_State
{
    public float rayLength = 0.3f; // Adjust the length of ray casted
    public float minSpeedThreshold = 1f; // Speed threshold for force adjustment
    public float minForce = 0.2f; // Minimum force applied
    public float maxForce = 10f; // Maximum force applied

    public override void EnterState(P_StateManager player)
    {
        player.anim.SetBool("Landing", true);
    }

    public override void UpdateState(P_StateManager player)
    {
        // Aiming at half speed
        float mouseX = Input.GetAxis("Mouse X") * 0.5f;
        float mouseY = Input.GetAxis("Mouse Y") * 0.5f;
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        player.rb.transform.Rotate(pitch, yaw, 0f, Space.Self);

        // Cast a ray forward from the player's position
        RaycastHit hit;
        Vector3 forward = player.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(player.transform.position, forward, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Meteor"))
            {
                // Calculate force magnitude based on player's current speed
                float currentSpeed = player.rb.velocity.magnitude;
                float forceMagnitude = Mathf.Lerp(minForce, maxForce, currentSpeed / minSpeedThreshold);

                // Clamp force magnitude between minForce and maxForce
                forceMagnitude = Mathf.Clamp(forceMagnitude, minForce, maxForce);

                // Apply force to the player in the direction they are facing
                player.rb.AddForce(forward * forceMagnitude * Time.deltaTime, ForceMode.Impulse);

                if (hit.distance < 0.1f)
                {
                    player.groundedObject = hit.transform;
                    player.SwitchState(player.groundedState);
                }
            }
        }

        if (!Input.GetMouseButton(0))
        {
            player.SwitchState(player.flyingState);
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.anim.SetBool("Landing", false);
    }
}
