using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FlyingState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        player.rb.drag = player.drag;
        player.HandGlow = false;
        // Reset camera angle form Grounded, Aiming or Pushing
        player.mainCamera.localRotation = Quaternion.identity;

        // Reset parent meteor form Grounded, Aiming or Pushing
        player.transform.SetParent(null);
        player.anim.SetBool("Flying", true);
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

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rollInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;
        bool shiftHeld = Input.GetKey(KeyCode.LeftShift);
        bool spaceHeld = Input.GetKey(KeyCode.Space);

        // Consume air
        if (player.oxygen > 0f)
        {
            if (horizontalInput != 0f)
            {
                player.oxygen -= player.oxygenConsumptionRate * Time.deltaTime;
            }
            if (verticalInput != 0f)
            {
                player.oxygen -= player.oxygenConsumptionRate * Time.deltaTime;
            }
            if (shiftHeld)
            {
                player.oxygen -= player.oxygenConsumptionRate * Time.deltaTime;
            }
            if (spaceHeld)
            {
                player.oxygen -= player.oxygenConsumptionRate * Time.deltaTime;
            }

            
            player.oxygen = Mathf.Clamp(player.oxygen, 0f, 100f);      // Ensure oxygen stays in bounds
            player.UpdateOxygenUI();
        }
        else
        {
            Debug.LogError("Oxygen depleted!");
        }


        // Rotation (pitch, yaw, roll)
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        float roll = rollInput * player.rollSpeed * Time.deltaTime;
        player.rb.transform.Rotate(pitch, yaw, roll, Space.Self);
        

        // Movement
        if (shiftHeld)
        {
            player.rb.AddRelativeForce(Vector3.forward * player.acceleration * Time.deltaTime, ForceMode.Acceleration);
            player.anim.SetBool("Fast-Fly", true);
            // TO DO: oxygen consumption
        }
        else if (!shiftHeld)
        {
            player.anim.SetBool("Fast-Fly", false);
        }

        if (spaceHeld)
        {
            player.rb.AddRelativeForce(-Vector3.forward * player.acceleration * Time.deltaTime, ForceMode.Acceleration);
        }

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        if (moveDirection != Vector3.zero)
        {
            player.rb.AddRelativeForce(moveDirection * player.acceleration * Time.deltaTime, ForceMode.Acceleration);
        }
        // Drag
        ApplyDrag(player);

        // State transition
        if (Input.GetMouseButton(0))
        {
            player.SwitchState(player.grabbingState);
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.anim.SetBool("Flying", false);
    }

    private void ApplyDrag(P_StateManager player)
    {
        Vector3 velocity = player.rb.velocity;
        Vector3 deceleration = -velocity.normalized * player.drag * Time.deltaTime;

        if (deceleration.magnitude > velocity.magnitude)
        {
            deceleration = -velocity;
        }

        player.rb.AddForce(deceleration, ForceMode.VelocityChange);
    }
}
