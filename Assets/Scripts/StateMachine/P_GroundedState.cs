using UnityEngine;

public class P_GroundedState : P_State
{
    private bool hasClamped = false;
    public override void EnterState(P_StateManager player)
    {
        // if(player.previousState is P_AimingState)
        // {
        //     player.transform.rotation = player.groundedPlayerRotation;
        //     player.mainCamera.rotation = Quaternion.Inverse(player.groundedPlayerRotation) * player.groundedCameraRotation;
        //     Debug.Log(("Enter", player.transform.rotation, player.mainCamera.rotation));
        // }
        player.rb.velocity = Vector3.zero;
        player.rb.isKinematic = true;
        player.transform.SetParent(player.groundedObject);
        player.anim.SetBool("Crawl-Idle", true);
        hasClamped = false;
    }

    public override void UpdateState(P_StateManager player)
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D keys
        float verticalInput = Input.GetAxis("Vertical");     // W/S keys


        MoveAlongSurface(player, horizontalInput, verticalInput);


        // Rotation (Mouse movement rotates the camera, not the player)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float pitch = -mouseY * player.rotationSpeed * Time.deltaTime;
        float yaw = mouseX * player.rotationSpeed * Time.deltaTime;
        player.mainCamera.Rotate(pitch, yaw, 0f, Space.Self);

        // Clamping the camera
        float xRotation = player.mainCamera.localEulerAngles.x;
        if (xRotation < 270f && xRotation > 2f)
        {
            if (!hasClamped)
            {
                Vector3 tempEulerAngles = player.mainCamera.localEulerAngles;
                tempEulerAngles.x = 2f;
                player.mainCamera.localEulerAngles = tempEulerAngles;
                hasClamped = true;
            }
            
            player.mainCamera.Rotate(-pitch, 0f, 0f, Space.Self);
        }

        // Rolling (Q and E keys rotate the player)
        float rollInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;
        if (rollInput != 0f)
        {
            player.transform.Rotate(0f, 0f, rollInput * player.rotationSpeed * 0.3f * Time.deltaTime, Space.Self);
        }

        AnimTracker(player);

        float angle = Vector3.Angle(player.transform.forward, player.mainCamera.forward);
        if (angle > 74f)
        {
            player.SwitchState(player.aimingState);
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            // Add boost in the opposite direction of player's pacing
            Vector3 oppositeDirection = -player.transform.forward;
            float boostForce = 0.2f; // Adjust for speed
            player.rb.isKinematic = false;
            player.rb.AddForce(oppositeDirection * boostForce, ForceMode.VelocityChange);

            player.SwitchState(player.flyingState);
            return;
        }

    }

    private void MoveAlongSurface(P_StateManager player, float horizontalInput, float verticalInput)
    {
        if(horizontalInput != 0f || verticalInput != 0f)
        {
            player.isCrawling = true;
        }
        else
        {
            player.isCrawling = false;
        }
        // Get the input
        Vector3 inputDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Transform the input direction to world space
        Vector3 worldInputDirection = player.transform.TransformDirection(inputDirection);

        // Set the raycast origin to head's position
        Vector3 raycastOrigin = player.transform.position;
        Vector3 raycastDirection = player.transform.forward;

        // Cast a ray
        RaycastHit hitInfo;
        float raycastDistance = 1f; // Adjust based on meteor's distance

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hitInfo, raycastDistance))
        {
            if (hitInfo.collider.CompareTag("Static Asteroid") || hitInfo.collider.CompareTag("Moving Asteroid"))
            {
                Vector3 surfaceNormal = hitInfo.normal;

                // Project the movement direction onto the surface tangent plane
                Vector3 movementDirection = Vector3.ProjectOnPlane(worldInputDirection, surfaceNormal).normalized;

                // Move the player
                Vector3 movement = movementDirection * player.moveSpeed * Time.deltaTime;
                player.transform.position += movement;

                // Adjust the player's position to maintain distance to surface
                float desiredDistance = 0.07f;
                float currentDistance = hitInfo.distance;
                float distanceDifference = desiredDistance - currentDistance;
                player.transform.position += surfaceNormal * distanceDifference;


                
                // Calculate the projected forward vector
                Vector3 projectedForward = Vector3.ProjectOnPlane(player.transform.forward, surfaceNormal).normalized;

                // Compute the target rotation
                Quaternion targetRotation = Quaternion.LookRotation(-surfaceNormal, player.transform.up);

                // Smoothly rotate the player towards the target rotation
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
            
        }
        else
        {
            Debug.Log("No ground detected!");
        }
    }

    public override void ExitState(P_StateManager player)
    {
        player.groundedPlayerRotation = player.transform.rotation;
        player.groundedCameraRotation = player.mainCamera.rotation;
        player.rb.isKinematic = false;
        player.anim.SetBool("Crawl-Idle", false);
        ResetAnims(player);

        player.isCrawling = false;

        // Reset relative rotation between player and camera
        //player.mainCamera.localRotation = Quaternion.identity;

        //Debug.Log(("Exit", player.transform.rotation, player.mainCamera.rotation));
    }

    private void AnimTracker(P_StateManager player)
    {
        int h = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        int v = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        string horizontal = h.ToString();
        string vertical = v.ToString();
        string c = horizontal + " " + vertical;
        //Debug.Log(c);
        switch (c)
        {
            case "-1 -1":
                ResetAnims(player);
                player.anim.SetBool("Move-Down-Left", true);
                break;
            case "-1 0":
                ResetAnims(player);
                player.anim.SetBool("Move-Left", true);
                break;
            case "-1 1":
                ResetAnims(player);
                player.anim.SetBool("Move-Up-Left", true);
                break;
            case "0 -1":
                ResetAnims(player);
                player.anim.SetBool("Move-Down", true);
                break;
            case "0 0":
                ResetAnims(player);
                player.anim.SetBool("Crawl-Idle", true);
                break;
            case "0 1":
                ResetAnims(player);
                player.anim.SetBool("Move-Up", true);
                break;
            case "1 -1":
                ResetAnims(player);
                player.anim.SetBool("Move-Down-Right", true);
                break;
            case "1 0":
                ResetAnims(player);
                player.anim.SetBool("Move-Right", true);
                break;
            case "1 1":
                ResetAnims(player);
                player.anim.SetBool("Move-Up-Right", true);
                break;
            default:
                ResetAnims(player);
                player.anim.SetBool("Crawl-Idle", true);
                break;
        }
    }

    private void ResetAnims(P_StateManager player)
    {
        player.anim.SetBool("Move-Up", false);
        player.anim.SetBool("Move-Up-Right", false);
        player.anim.SetBool("Move-Right", false);
        player.anim.SetBool("Move-Down-Right", false);
        player.anim.SetBool("Move-Down", false);
        player.anim.SetBool("Move-Down-Left", false);
        player.anim.SetBool("Move-Left", false);
        player.anim.SetBool("Move-Up-Left", false);
        player.anim.SetBool("Crawl-Idle", false);
    }
}
