using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    #region flip manager
    [SerializeField] private bool posHeight;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    float speed = .2f;
    float timeCount = 0.0f;
    #endregion

    #region line values
    [SerializeField] private Color c1 = Color.yellow;
    [SerializeField] private LineRenderer lineRenderer;
    #endregion

    #region tracking objects
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerTracker;
    [SerializeField] GameObject trackerAnchor;
    [SerializeField] GameObject[] asteroids;
    #endregion


    void Start()
    {
        lineRenderer.startColor = Color.white;
        lineRenderer.startWidth = .01f;
        var points = new Vector3[2];
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
        posHeight = true;
        for (int i = 0; i < asteroids.Length; i++)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCount = timeCount + Time.deltaTime;
        TrackerUpdate();
        MakeLine();
        CheckHeight();
    }

    private void CheckHeight()
    {
        if (posHeight == true && player.transform.position.y < 0)
        {
            posHeight = false;
        }
        else if (posHeight ==false && player.transform.position.y > 0)
        {
            posHeight = true;
        }
        if (posHeight == true)
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, to.rotation, Time.deltaTime * 30f);
        else if (posHeight == false)
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, from.rotation, Time.deltaTime * 30f);
    }

    private void TrackerUpdate()
    {
        playerTracker.transform.localRotation = player.transform.rotation;
        playerTracker.transform.localPosition = new Vector3(player.transform.position.x * 0.05f, player.transform.position.y * 0.05f, player.transform.position.z * 0.05f);
        trackerAnchor.transform.localPosition = new Vector3(player.transform.position.x * 0.05f, 0, player.transform.position.z * 0.05f);
    }

    private void MakeLine()
    {
        var points = new Vector3[2];
        lineRenderer.startWidth = .01f;
        lineRenderer.endWidth = .01f;
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
    }
}
