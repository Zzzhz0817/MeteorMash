using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float thrustPower = 0.1f; // Speed when using oxygen
    public float bouncePower = 3f; // Speed after pressing space on a meteor
    public float oxygenDecreaseRate = 1f; // Oxygen reduction rate over time
    public float oxygenConsumptionRate = 5f; // Oxygen consumption rate when accelerating
    public float maxOxygen = 100f; // Maximum oxygen amount
    public Transform cameraTransform; // Reference to the player camera
    public Slider oxygenBar; // UI for oxygen bar

    private Rigidbody rb;
    private float currentOxygen;
    private bool isOnMeteor = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.drag = 0; // No drag in space
        currentOxygen = maxOxygen;
        oxygenBar.maxValue = maxOxygen;
        oxygenBar.value = currentOxygen;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse rotation for looking around
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraTransform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.right * -mouseY);

        // Oxygen decrease over time
        if (currentOxygen > 0)
        {
            currentOxygen -= oxygenDecreaseRate * Time.deltaTime;
            oxygenBar.value = currentOxygen;
        }

        // Launch from meteor when pressing space
        if (isOnMeteor && Input.GetKeyDown(KeyCode.Space) && currentOxygen > 0)
        {
            rb.velocity = cameraTransform.forward * bouncePower;
            isOnMeteor = false;
        }

        // Accelerate with left mouse button if oxygen available
        if (Input.GetMouseButton(0) && currentOxygen > 0)
        {
            rb.AddForce(cameraTransform.forward * thrustPower, ForceMode.Acceleration);
            currentOxygen -= oxygenConsumptionRate * Time.deltaTime;
            oxygenBar.value = currentOxygen;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Meteor"))
        {
            rb.velocity = Vector3.zero; // Stop movement when hitting a meteor
            rb.angularVelocity = Vector3.zero; // Stop any rotation
            isOnMeteor = true;
        }
    }
}
