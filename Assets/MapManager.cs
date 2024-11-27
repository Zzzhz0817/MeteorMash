using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEditor.PlayerSettings;

public class MapManager : MonoBehaviour
{

    #region flip manager
    [SerializeField] private bool posHeight;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private float speed = .2f;
    [SerializeField] private float timeCount = 0.0f;
    #endregion

    #region line values
    [SerializeField] private LineRenderer lineRenderer;
    #endregion

    #region tracking objects
    [SerializeField] private float sizer = 0.015f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerTracker;
    [SerializeField] private GameObject trackerAnchor;
    [SerializeField] private GameObject[] asteroids;
    [SerializeField] private GameObject[] missions;
    [SerializeField] private GameObject[] ship;
    [SerializeField] private GameObject prefabAsset;
    [SerializeField] private GameObject missionAsset;
    [SerializeField] private GameObject shipAsset;
    [SerializeField] private GameObject parentLadder;
    #endregion




    void Start()
    {
        LineSetup();
        posHeight = true;
        BuildMap();
    }


    // Update is called once per frame
    void Update()
    {
        timeCount = timeCount + Time.deltaTime;
        TrackerUpdate();
        MakeLine();
        CheckHeight();
    }

    private void BuildMap()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Meteor");
        foreach (GameObject objects in asteroids)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject obj = Instantiate(prefabAsset, waypoint, transform.rotation) as GameObject;
            obj.transform.SetParent(parentLadder.transform);
            obj.transform.localPosition = new Vector3(objects.transform.position.x * sizer, objects.transform.position.y * sizer, objects.transform.position.z * sizer);
        }
        foreach (GameObject poi in missions)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject mission = Instantiate(missionAsset, waypoint, transform.rotation) as GameObject;
            mission.transform.SetParent(parentLadder.transform);
            mission.transform.localPosition = new Vector3(poi.transform.position.x * sizer, poi.transform.position.y * sizer, poi.transform.position.z * sizer);
        }
        foreach (GameObject ship in ship)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject goal = Instantiate(shipAsset, waypoint, transform.rotation) as GameObject;
            goal.transform.SetParent(parentLadder.transform);
            goal.transform.localPosition = new Vector3(ship.transform.position.x * sizer, ship.transform.position.y * sizer, ship.transform.position.z * sizer);
        }
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
            transform.rotation = Quaternion.Lerp(this.transform.rotation, to.rotation, Time.deltaTime * 5f);
        else if (posHeight == false)
            transform.rotation = Quaternion.Lerp(this.transform.rotation, from.rotation, Time.deltaTime * 5f);
    }

    private void TrackerUpdate()
    {
        playerTracker.transform.rotation = player.transform.rotation;
        playerTracker.transform.localPosition = new Vector3(player.transform.position.x * sizer, player.transform.position.y * sizer, player.transform.position.z * sizer);
        trackerAnchor.transform.localPosition = new Vector3(player.transform.position.x * sizer, 0, player.transform.position.z * sizer);
    }

    private void LineSetup()
    {
        lineRenderer.startColor = Color.green;
        lineRenderer.startWidth = .02f;
        var points = new Vector3[2];
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
    }
    private void MakeLine()
    {
        var points = new Vector3[2];
        lineRenderer.startWidth = .03f;
        lineRenderer.endWidth = .03f;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
    }
}
