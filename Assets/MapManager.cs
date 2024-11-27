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
    [SerializeField] private Color c1 = Color.yellow;
    [SerializeField] private LineRenderer lineRenderer;
    #endregion

    #region tracking objects
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
        lineRenderer.startColor = Color.white;
        lineRenderer.startWidth = .01f;
        var points = new Vector3[2];
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
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
            Debug.Log("I've made a Meteor");
            obj.transform.localPosition = new Vector3(objects.transform.position.x * 0.02f, objects.transform.position.y * 0.02f, objects.transform.position.z * 0.02f);
        }
        foreach (GameObject poi in missions)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject mission = Instantiate(missionAsset, waypoint, transform.rotation) as GameObject;
            mission.transform.SetParent(parentLadder.transform);
            Debug.Log("I've made a POI");
            mission.transform.localPosition = new Vector3(poi.transform.position.x * 0.02f, poi.transform.position.y * 0.02f, poi.transform.position.z * 0.02f);
        }
        foreach (GameObject ship in ship)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject goal = Instantiate(shipAsset, waypoint, transform.rotation) as GameObject;
            goal.transform.SetParent(parentLadder.transform);
            Debug.Log("I've made the ship");
            goal.transform.localPosition = new Vector3(ship.transform.position.x * 0.02f, ship.transform.position.y * 0.02f, ship.transform.position.z * 0.02f);
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
        playerTracker.transform.localPosition = new Vector3(player.transform.position.x * 0.02f, player.transform.position.y * 0.02f, player.transform.position.z * 0.02f);
        trackerAnchor.transform.localPosition = new Vector3(player.transform.position.x * 0.02f, 0, player.transform.position.z * 0.02f);
    }

    private void MakeLine()
    {
        var points = new Vector3[2];
        lineRenderer.startWidth = .03f;
        lineRenderer.endWidth = .03f;
        points[0] = playerTracker.transform.position;
        points[1] = trackerAnchor.transform.position;
        lineRenderer.SetPositions(points);
        lineRenderer.SetColors(Color.green, Color.green);
    }
}
