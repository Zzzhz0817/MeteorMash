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

    #region map manager
    [SerializeField] private bool posHeight;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private float mapScalar = .02f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timeCount = 0.0f;
    #endregion

    #region line values
    [SerializeField] private Color c1 = Color.yellow;
    [SerializeField] private LineRenderer lineRenderer;
    #endregion

    #region tracking objects
    [SerializeField] private P_StateManager player;
    [SerializeField] private GameObject playerTracker;
    [SerializeField] private GameObject trackerAnchor;
    [SerializeField] private GameObject[] staticAsteroids;
    [SerializeField] private GameObject[] movingAsteroids;
    [SerializeField] private List<GameObject> movingAsteroidTracker;
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
        staticAsteroids = GameObject.FindGameObjectsWithTag("Static Asteroid");
        foreach (GameObject objects in staticAsteroids)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject obj = Instantiate(prefabAsset, waypoint, transform.rotation) as GameObject;
            obj.transform.SetParent(parentLadder.transform);
            Debug.Log("I've made a static asteroid");
            obj.transform.localPosition = new Vector3(objects.transform.position.x * mapScalar, objects.transform.position.y * mapScalar, objects.transform.position.z * mapScalar);
        }
        movingAsteroids = GameObject.FindGameObjectsWithTag("Moving Asteroid");
        foreach (GameObject objects in movingAsteroids)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject obj = Instantiate(prefabAsset, waypoint, transform.rotation) as GameObject;
            obj.transform.SetParent(parentLadder.transform);
            movingAsteroidTracker.Add(obj);
            Debug.Log("I've made a moving asteroid");
            obj.transform.localPosition = new Vector3(objects.transform.position.x * mapScalar, objects.transform.position.y * mapScalar, objects.transform.position.z * mapScalar);
        }
        foreach (GameObject poi in missions)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject mission = Instantiate(missionAsset, waypoint, transform.rotation) as GameObject;
            mission.transform.SetParent(parentLadder.transform);
            Debug.Log("I've made a POI");
            mission.transform.localPosition = new Vector3(poi.transform.position.x * mapScalar, poi.transform.position.y * mapScalar, poi.transform.position.z * mapScalar);
        }
        foreach (GameObject ship in ship)
        {
            Vector3 waypoint = new Vector3(0, 0, 0);
            GameObject goal = Instantiate(shipAsset, waypoint, transform.rotation) as GameObject;
            goal.transform.SetParent(parentLadder.transform);
            Debug.Log("I've made the ship");
            goal.transform.localPosition = new Vector3(ship.transform.position.x * mapScalar, ship.transform.position.y * mapScalar, ship.transform.position.z * mapScalar);
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
        playerTracker.transform.localPosition = new Vector3(player.transform.position.x * mapScalar, player.transform.position.y * mapScalar, player.transform.position.z * mapScalar);
        trackerAnchor.transform.localPosition = new Vector3(player.transform.position.x * mapScalar, 0, player.transform.position.z * mapScalar);
        for (int i = 0; i < movingAsteroidTracker.Count; i++)
        {
            movingAsteroidTracker[i].transform.localPosition = new Vector3(movingAsteroids[i].transform.transform.position.x * mapScalar, movingAsteroids[i].transform.position.y * mapScalar, movingAsteroids[i].transform.position.z * mapScalar);
        }
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
