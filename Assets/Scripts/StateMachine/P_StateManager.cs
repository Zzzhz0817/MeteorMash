using System.Collections;
using UnityEngine;

public class P_StateManager : MonoBehaviour
{
    #region Hand Glow
    public bool HandGlow = false;
    public ParticleSystem HandGlowL;
    public ParticleSystem HandGlowR;
    #endregion

    #region States
    public P_State currentState;
    public P_State previousState;
    public P_FlyingState flyingState = new P_FlyingState();
    public P_GroundedState groundedState = new P_GroundedState();
    public P_GrabbingState grabbingState = new P_GrabbingState();
    public P_AimingState aimingState = new P_AimingState();
    public P_PushingState pushingState = new P_PushingState();
    public P_DialogueState dialogueState = new P_DialogueState();
    public P_PausedState pausedState = new P_PausedState();
    #endregion

    #region Components
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Transform groundedObject;
    public Transform mainCamera;
    [HideInInspector] public Quaternion groundedPlayerRotation;
    [HideInInspector] public Quaternion groundedCameraRotation;
    [HideInInspector] public Animator anim;
    public GameObject laser;
    #endregion

    #region Movement Variables
    public float moveSpeed = 10f;
    public float rotationSpeed = 500f;
    public float rollSpeed = 50f;
    public float acceleration = 50f;
    public float drag = 0.1f;
    #endregion

    #region Resource Variables
    public float oxygen = 100f;
    public float power = 0f;
    public float oxygenConsumptionRate = 1f; // Amount of oxygen to consume per second
    private Coroutine oxygenConsumptionCoroutine;
    #endregion

    #region UI Elements
    public OxygenSlideBar oxygenSlideBar;
    public PowerSlideBar powerSlideBar;
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        currentState = flyingState;

        currentState.EnterState(this);
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        mainCamera = transform.Find("Main Camera");

        // Initialize UI
        UpdateOxygenUI();
        UpdatePowerUI();

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found as a child of the player!");
            return;
        }
    }

    private void Update()
    {
        currentState.UpdateState(this);
        if (HandGlow == false)
        {
            HandGlowL.Stop();
            HandGlowR.Stop();
        }
        else
        {
            HandGlowL.Play();
            HandGlowR.Play();
        }
        if (mainCamera != null)
        {
            // Get the current local rotation
            Quaternion currentRotation = mainCamera.localRotation;

            // Lock the Z-axis rotation to 0
            mainCamera.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0f);
        }

        if (oxygen > 0f)
        {
            oxygen -= oxygenConsumptionRate * Time.deltaTime; // Consume oxygen at a rate
            oxygen = Mathf.Clamp(oxygen, 0f, 100f);      // Ensure oxygen stays in bounds
            UpdateOxygenUI();
        }
        else
        {
            Debug.LogError("Oxygen depleted!");
        }
    }

    public void SwitchState(P_State state)
    {
        Debug.Log($"Switching to {state.GetType().Name}");
        currentState.ExitState(this);
        previousState = currentState;
        currentState = state;
        state.EnterState(this);
    }

    public void SwitchToPreviousState()
    {
        if (previousState != null)
        {
            SwitchState(previousState);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    /// <summary>
    /// Public method to set power.
    /// </summary>
    /// <param name="amount">New power value.</param>
    public void SetPower(float amount)
    {
        power = Mathf.Clamp(amount, 0f, 100f); // Ensure power stays within 0-100
        UpdatePowerUI();
    }

    /// <summary>
    /// Public method to add oxygen.
    /// </summary>
    /// <param name="amount">Amount of oxygen to add (can be negative).</param>
    public void AddOxygen(float amount)
    {
        oxygen += amount;
        oxygen = Mathf.Clamp(oxygen, 0f, 100f); // Ensure oxygen stays within 0-100
        UpdateOxygenUI();
    }

    /// <summary>
    /// Updates the oxygen bar in the UI.
    /// </summary>
    public void UpdateOxygenUI()
    {
        if (oxygenSlideBar != null)
        {
            oxygenSlideBar.SetOxygen(oxygen);
        }
        else
        {
            Debug.LogWarning("OxygenSlideBar not assigned in the Inspector.");
        }
    }

    /// <summary>
    /// Updates the power bar in the UI.
    /// </summary>
    public void UpdatePowerUI()
    {
        if (powerSlideBar != null)
        {
            powerSlideBar.SetPower(power);
        }
        else
        {
            Debug.LogWarning("PowerSlideBar not assigned in the Inspector.");
        }
    }
}
