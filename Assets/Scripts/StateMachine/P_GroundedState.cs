using UnityEngine;

public class P_GroundedState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        //enable grounded movement
        player.rb.isKinematic = true;
        player.transform.SetParent(player.groundedObject);

        AlignWithSurfaceNormal(player);
    }

    public override void UpdateState(P_StateManager player)
    {
        /*TO DO: 
        
        if(PlayerControl.releases left mouse button)
        {
            player.SwitchState(player.flyingState);

        }
            if(PlayerControl.presses both mouse buttons)
        {
            player.SwitchState(player.pushingState);

        }
            if(PlayerControl.aim greater than 90 degrees away from center of object)
        {
            player.SwitchState(player.aimingState);

        }
        */
        
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D keys
        float verticalInput = Input.GetAxis("Vertical");     // W/S keys

        // Check for movement input
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            MoveAlongSphere(player, horizontalInput, verticalInput);
        }

        // Rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        player.transform.Rotate(pitch, yaw, 0f, Space.Self);

        // Rolling
        float rollInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;
        if (rollInput != 0f)
        {
            player.transform.Rotate(0f, 0f, rollInput * player.rotationSpeed * 0.3f * Time.deltaTime, Space.Self);
        }

        // Check for aiming
        Vector3 toCenter = player.groundedObject.position - player.transform.position;
        float angle = Vector3.Angle(player.transform.forward, toCenter);
        if (angle > 90f)
        {
            player.SwitchState(player.aimingState);
            return;
        }

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
        player.transform.SetParent(null);
        player.rb.isKinematic = false;
    }

    private void MoveAlongSphere(P_StateManager player, float horizontalInput, float verticalInput)
    {
        Vector3 normal = (player.transform.position - player.groundedObject.position).normalized;
        float sphereRadius = Vector3.Distance(player.transform.position, player.groundedObject.position);

        Vector3 tangentRight = Vector3.Cross(Vector3.up, normal).normalized;
        Vector3 tangentUp = -Vector3.Cross(normal, tangentRight).normalized;

        Vector3 movementDirection = (tangentRight * horizontalInput + tangentUp * verticalInput).normalized;
        Vector3 rotationAxis = Vector3.Cross(movementDirection, normal);

        float angle = (player.moveSpeed * Time.deltaTime / sphereRadius) * Mathf.Rad2Deg;
        player.transform.RotateAround(player.groundedObject.position, rotationAxis, angle);

        AlignWithSurfaceNormal(player);
    }

    private void AlignWithSurfaceNormal(P_StateManager player)
    {
        Vector3 normal = (player.transform.position - player.groundedObject.position).normalized;

        player.transform.forward = -normal;
    }
}
