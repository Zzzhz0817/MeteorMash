using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private AudioSource breathingAudioSource;
    private AudioSource backgroundMusicAudioSource;
    private AudioSource crawlingAudioSource;
    private AudioSource oxygenBoostingAudioSource;
    private AudioSource grabbingAudioSource;
    private AudioSource otherSoundsAudioSource;

    public AudioClip[] breathingClips;
    public AudioClip[] backgroundMusicClips;
    public AudioClip idCardClip, laserCutterClip, sealantSprayClip, oxygenRefillClip,
            landingClip, crawlingClip, oxygenBoostingClip, grabbingClip, suffocationClip;
    #endregion

    #region Movement Variables
    public float moveSpeed = 10f;
    public float rotationSpeed = 500f;
    public float rollSpeed = 50f;
    public float acceleration = 50f;
    public float drag = 0.2f;
    #endregion

    #region Resource Variables
    public float oxygen = 100f;
    public float power = 0f;
    public float oxygenConsumptionRate = 0.2f; // Amount of oxygen to consume per second
    public float boostMult = 0f;
    private Coroutine oxygenConsumptionCoroutine;
    public bool isCrawling = false;
    public bool isBoosting = false;
    public bool isGrabbing = false;
    private bool isSuffocating = false;
    #endregion

    #region UI Elements
    public OxygenSlideBar oxygenSlideBar;
    public PowerSlideBar powerSlideBar;
    public CanvasGroup fadePanel;
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
        
        // Initialize audio sources
        breathingAudioSource = gameObject.AddComponent<AudioSource>();
        breathingAudioSource.loop = false;
        breathingAudioSource.volume = 0.075f;

        backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicAudioSource.loop = false;
        backgroundMusicAudioSource.volume = 0.2f;


        crawlingAudioSource = gameObject.AddComponent<AudioSource>();
        crawlingAudioSource.loop = true;
        crawlingAudioSource.volume = 0f;
        crawlingAudioSource.clip = crawlingClip;
        crawlingAudioSource.Play();

        oxygenBoostingAudioSource = gameObject.AddComponent<AudioSource>();
        oxygenBoostingAudioSource.loop = true;
        oxygenBoostingAudioSource.volume = 0f;
        oxygenBoostingAudioSource.clip = oxygenBoostingClip;
        oxygenBoostingAudioSource.Play();

        grabbingAudioSource = gameObject.AddComponent<AudioSource>();
        grabbingAudioSource.loop = true;
        grabbingAudioSource.volume = 0f;
        grabbingAudioSource.clip = grabbingClip;
        grabbingAudioSource.Play();

        otherSoundsAudioSource = gameObject.AddComponent<AudioSource>();
        otherSoundsAudioSource.loop = false;

        StartCoroutine(PlayBreathingSound());
        StartCoroutine(PlayBackgroundMusic());

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found as a child of the player!");
            return;
        }
    }

    private void Update()
    {
        currentState.UpdateState(this);
        
        // Hand Glow Logic
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

        // Lock Camera Rotation on Z-axis
        if (mainCamera != null)
        {
            Quaternion currentRotation = mainCamera.localRotation;
            mainCamera.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0f);
        }

        // Oxygen Logic
        if (oxygen > 0f)
        {
            oxygen -= oxygenConsumptionRate * Time.deltaTime;
            oxygen = Mathf.Clamp(oxygen, 0f, 100f);
            UpdateOxygenUI();

            // Adjust breathing sound volume
            breathingAudioSource.volume = oxygen < 20f ? 0.3f : 0.1f;
        }
        else if (!isSuffocating)
        {
            StartCoroutine(SuffocationSequence());
            isSuffocating = true;
        }

        // Update Audio Volumes
        crawlingAudioSource.volume = isCrawling ? 1f : 0f;
        oxygenBoostingAudioSource.volume = isBoosting ? 1f : 0f;
        grabbingAudioSource.volume = isGrabbing ? 1f : 0f;
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
    
    private IEnumerator PlayBreathingSound()
    {
        while (true)
        {
            breathingAudioSource.clip = breathingClips[Random.Range(0, breathingClips.Length)];
            breathingAudioSource.Play();

            yield return new WaitForSeconds(breathingAudioSource.clip.length);
        }
    }

    private IEnumerator PlayBackgroundMusic()
    {
        while (true)
        {
            if (oxygen < 50f)
            {
                backgroundMusicAudioSource.clip = backgroundMusicClips[2];
            }
            else
            {
                backgroundMusicAudioSource.clip = backgroundMusicClips[Random.Range(0, 2)];
            }

            backgroundMusicAudioSource.Play();

            while (backgroundMusicAudioSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    private IEnumerator SuffocationSequence()
    {
        PlaySound("Suffocation");

        float fadeDuration = 4f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            if (fadePanel != null)
            {
                fadePanel.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Fade in
            }
            yield return null;
        }

        // Load the death scene
        SceneManager.LoadScene("Menu-Oxygen-Dead");
    }


    public void PlaySound(string soundName)
    {
        AudioClip clip = null;

        switch (soundName)
        {
            case "IDCard":
                otherSoundsAudioSource.volume = .3f;
                clip = idCardClip;
                break;
            case "LaserCutter":
                otherSoundsAudioSource.volume = .3f;
                clip = laserCutterClip;
                break;
            case "SealantSpray":
                otherSoundsAudioSource.volume = .3f;
                clip = sealantSprayClip;
                break;
            case "OxygenRefill":
                otherSoundsAudioSource.volume = .3f;
                clip = oxygenRefillClip;
                break;
            case "Landing":
                otherSoundsAudioSource.volume = .3f;
                clip = landingClip;
                break;
            case "Suffocation":
                otherSoundsAudioSource.volume = .3f;
                clip = suffocationClip;
                break;
        }

        if (clip != null)
        {
            otherSoundsAudioSource.PlayOneShot(clip);
        }
    }
}
