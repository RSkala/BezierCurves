using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] GameObject P0;
    [SerializeField] GameObject P0_ControlPoint;
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P1_ControlPoint;
    [SerializeField][Range(1, 100)] int numLineSegments = 50;
    [SerializeField][Range(0.01f, 1.0f)] float lineWidth = 0.25f;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer == null)
        {
            Debug.LogError("Invalid lineRenderer on " + gameObject.name);
            lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
        }
        CheckValidControlPoints();
    }

    void CheckValidControlPoints()
    {
        if(P0 == null)
        {
            Debug.LogError("P0 is null");
        }

        if(P0_ControlPoint == null)
        {
            Debug.LogError("P0_ControlPoint is null");
        }

        if(P1 == null)
        {
            Debug.LogError("P1 is null");
        }

        if(P1_ControlPoint == null)
        {
            Debug.LogError("P1_ControlPoint is null");
        }
    }

    void Update()
    {
        UpdateLineWidth();
        DrawBezierCurve();
    }

    void UpdateLineWidth()
    {
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
    }

    void DrawBezierCurve()
    {
        // The number of positions will always be one more than the number of segments, so add 1 to the number of segments
        lineRenderer.positionCount = numLineSegments + 1;

        // Iterate through the number of line positions indexes so we can get the "t" values for t=0 through t=1.
        for(int i = 0; i < lineRenderer.positionCount; ++i)
        {
            float t = i / (float)numLineSegments;

            Vector3 bezierPoint = CalculateCubicBezierPoint(t, P0.transform.position, P0_ControlPoint.transform.position, P1_ControlPoint.transform.position, P1.transform.position);
            lineRenderer.SetPosition(i, bezierPoint);
        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //------------------------------------------------------
        // Cubic Bezier Curve Forumula:
        //
        // (1-t)^3 * P0 +
        // 3*(1-t)^2 * t*P1 +
        // 3*(1-t) * t^2 * P2 +
        // t^3 * P3 
        //------------------------------------------------------
        float oneMinusT = 1 - t;
        float oneMinusTSquared = oneMinusT * oneMinusT;
        float oneMinusTCubed = oneMinusTSquared * oneMinusT;

        float tSquared = t * t;
        float tCubed = tSquared * t;

        Vector3 bezierPoint = oneMinusTCubed * p0;
        bezierPoint += 3 * oneMinusTSquared * t * p1;
        bezierPoint += 3 * oneMinusT * tSquared * p2;
        bezierPoint += tCubed * p3;

        return bezierPoint;
    }
}
