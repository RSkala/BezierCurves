using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    [SerializeField]
    GameObject controlPoint1;

    [SerializeField]
    GameObject controlPoint2;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineEffect();
    }

    void UpdateLineEffect()
    {
        //lineRenderer.positionCount = 51; // Number of segments + 1
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, controlPoint1.transform.position);
        lineRenderer.SetPosition(1, controlPoint2.transform.position);
        // for(int i = 0; i < lineRenderer.positionCount; ++i)
        // {

        // }
    }
}
