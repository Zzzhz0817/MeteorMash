using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineTracker : MonoBehaviour
{
   
    [SerializeField] private Color c1 = Color.yellow;
    [SerializeField] private Transform sPos;
    [SerializeField] private Transform ePos;
    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(.01f, .01f);
        var points = new Vector3[2];
        points[0] = sPos.position;
        points[1] = ePos.position;
        lineRenderer.SetPositions(points);
    }

    void Update()
    {
        var points = new Vector3[2];
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(.01f, .01f);
        points[0] = sPos.position;
        points[1] = ePos.position;
        lineRenderer.SetPositions(points);
    }
}
