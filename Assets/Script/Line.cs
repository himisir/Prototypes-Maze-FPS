using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetUp(Vector3 currentPos, Vector3 targetPos)
    {
        var distance = Vector3.Distance(currentPos, targetPos);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, currentPos);
        lineRenderer.SetPosition(1, targetPos);
    }
}
