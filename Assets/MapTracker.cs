using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTracker : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool getHeight;

    void Start()
    {

    }
    void Update()
    {
        this.transform.localRotation = player.transform.rotation;
        if (getHeight)
        {
            this.transform.localPosition = new Vector3(player.transform.position.x * 0.05f, player.transform.position.y * 0.05f, player.transform.position.z * 0.05f);
        }
        else
        {
            this.transform.localPosition = new Vector3(player.transform.position.x * 0.05f, 0, player.transform.position.z * 0.05f);
        }
    }
}
