using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float rotationZ = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationX * Time.deltaTime, rotationY * Time.deltaTime, rotationZ * Time.deltaTime);
    }
}
