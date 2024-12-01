using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingAstroids : MonoBehaviour
{
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate around the Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Rotate all child objects in the opposite direction
        foreach (Transform child in transform)
        {
            child.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
