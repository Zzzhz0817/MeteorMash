using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float boostForce = 10f;
    public float surfaceMoveSpeed = 5f;
    public float maxPullSpeed = 10f;
    public float pullAcceleration = 2f;
    public float detachForce = 2f;
    public float cameraResetSpeed = 1f;
    public float maxCameraAngle = 100f;

    private Rigidbody rb;
    private Transform cameraTransform;
    private bool isGrabbing = false;
    private float pullSpeed = 0f;
    private GameObject currentMeteor;
    private Vector3 meteorCenter;
    private Vector3 meteorNormal;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        // Bind actions to functions
        controls.Player.Boost.performed += ctx => Boost();
        controls.Player.GrabLeft.performed += ctx => TryGrabMeteor();
        controls.Player.GrabRight.performed += ctx => TryGrabMeteor();
        controls.Player.GrabLeft.canceled += ctx => CheckGrabRelease();
        controls.Player.GrabRight.canceled += ctx => CheckGrabRelease();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 rotateInput = controls.Player.Rotate.ReadValue<Vector2>();
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();

        if (isGrabbing)
        {
            SurfaceMovement(moveInput);
            AdjustCamera(rotateInput);
            HandlePulling();
        }
        else
        {
            FreeRotate(rotateInput);
        }
    }

    private void FreeRotate(Vector2 rotateInput)
    {
        float rotateX = rotateInput.x * rotationSpeed * Time.deltaTime;
        float rotateY = rotateInput.y * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotateY, rotateX, 0);
    }

    private void Boost()
    {
        rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
    }

    private void SurfaceMovement(Vector2 moveInput)
    {
        if (currentMeteor == null) return;

        Vector3 right = Vector3.Cross(meteorNormal, Vector3.up).normalized;
        Vector3 forward = Vector3.Cross(meteorNormal, right).normalized;

        Vector3 moveDirection = (right * moveInput.x + forward * moveInput.y).normalized;
        rb.MovePosition(rb.position + moveDirection * surfaceMoveSpeed * Time.deltaTime);
    }

    private void AdjustCamera(Vector2 rotateInput)
    {
        Vector3 targetDirection = (meteorCenter - transform.position).normalized;
        Quaternion defaultRotation = Quaternion.LookRotation(targetDirection, meteorNormal);

        float angleBetween = Quaternion.Angle(cameraTransform.rotation, defaultRotation);

        if (rotateInput == Vector2.zero && angleBetween > maxCameraAngle)
        {
            cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, defaultRotation, cameraResetSpeed * Time.deltaTime);
        }
        else
        {
            float rotateX = rotateInput.x * rotationSpeed * Time.deltaTime;
            float rotateY = rotateInput.y * rotationSpeed * Time.deltaTime;
            cameraTransform.Rotate(rotateY, rotateX, 0);
        }
    }

    private void HandlePulling()
    {
        bool grabLeft = controls.Player.GrabLeft.IsPressed();
        bool grabRight = controls.Player.GrabRight.IsPressed();

        if (grabLeft && grabRight)
        {
            pullSpeed = Mathf.Min(pullSpeed + pullAcceleration * Time.deltaTime, maxPullSpeed);
            Vector3 pullDirection = (meteorCenter - transform.position).normalized;
            rb.MovePosition(transform.position + pullDirection * pullSpeed * Time.deltaTime);
        }
    }

    private void CheckGrabRelease()
    {
        bool grabLeft = controls.Player.GrabLeft.IsPressed();
        bool grabRight = controls.Player.GrabRight.IsPressed();

        if (!grabLeft && !grabRight && isGrabbing)
        {
            ReleaseMeteor();
        }
        else if (!grabLeft && !grabRight && isGrabbing)
        {
            JumpFromMeteor();
        }
    }

    private void TryGrabMeteor()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider obj in nearbyObjects)
        {
            if (obj.CompareTag("Meteor"))
            {
                currentMeteor = obj.gameObject;
                meteorCenter = currentMeteor.transform.position;
                meteorNormal = (transform.position - meteorCenter).normalized;

                isGrabbing = true;
                rb.velocity = Vector3.zero;
                return;
            }
        }
    }

    private void ReleaseMeteor()
    {
        if (!isGrabbing) return;

        isGrabbing = false;
        Vector3 detachDirection = (transform.position - meteorCenter).normalized;
        rb.AddForce(detachDirection * detachForce, ForceMode.Impulse);
        pullSpeed = 0;
        currentMeteor = null;
    }

    private void JumpFromMeteor()
    {
        if (!isGrabbing) return;

        Vector3 jumpDirection = cameraTransform.forward;
        rb.AddForce(jumpDirection * pullSpeed, ForceMode.Impulse);

        isGrabbing = false;
        pullSpeed = 0;
        currentMeteor = null;
    }
}
