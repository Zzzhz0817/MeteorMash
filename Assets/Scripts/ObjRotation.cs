using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    public Vector3 selfPos;
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float rotationZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        selfPos = transform.position;
        rotationX = transform.position.x;
        rotationY = transform.position.y;
        rotationZ = transform.position.z;
        rotationX = Mathf.Abs(rotationX % 8) - 4;
        rotationY = Mathf.Abs(rotationY % 8) - 4;
        rotationZ = Mathf.Abs(rotationZ % 8) - 4;


    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationX * Time.deltaTime, rotationY * Time.deltaTime, rotationZ * Time.deltaTime);
    }
}
