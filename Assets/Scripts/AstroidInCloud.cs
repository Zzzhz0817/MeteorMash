using UnityEngine;

public class AstroidInCloud : MonoBehaviour
{
    private Transform blackHole;

    public float baseRotationSpeed = 10f; // Base speed of orbit
    public float gravitationalConstant = 1f; // A multiplier for gravity influence
    public Vector3 selfRotationSpeed = new Vector3(0f, 0f, 0f); // Self-rotation speed
    private Vector3 orbitAxis; // Orbit inclination (Euler angles)

    private void Start()
    {
        // Ensure the black hole is the parent object
        blackHole = transform.parent;

        if (blackHole == null)
        {
            Debug.LogError("Asteroid must have a parent object representing the black hole!");
        }


        Vector3 toAsteroid = transform.position - blackHole.position;

        // A perpendicular vector for the orbit axis
        orbitAxis = Vector3.Cross(toAsteroid.normalized, Vector3.forward);
        if (orbitAxis == Vector3.zero)
        {
            orbitAxis = Vector3.Cross(toAsteroid.normalized, Vector3.up);
        }
        orbitAxis = orbitAxis.normalized;
    }

    private void Update()
    {

        // Calculate the multiplier for the rotation speed based on distance (Inverse Square Law)
        float distance = Vector3.Distance(transform.position, blackHole.position);
        float orbitSpeedMultiplier = gravitationalConstant / Mathf.Pow(distance, 1.5f);

        // Orbiting
        transform.RotateAround(
            blackHole.position,
            orbitAxis,
            baseRotationSpeed * orbitSpeedMultiplier * Time.deltaTime
        );

        // Self-rotation
        transform.Rotate(selfRotationSpeed * Time.deltaTime);
    }
}
