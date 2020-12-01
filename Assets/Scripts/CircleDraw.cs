using UnityEngine;
using System.Collections;

public class CircleDraw : MonoBehaviour
{
    public GameObject centerPoint;
    public Material mat;
    float theta_scale = 0.05f;        //Set lower to add more points
    int size; //Total number of points in circle
    float radius;
    LineRenderer lineRenderer;

    void Awake()
    {
        radius = (centerPoint.transform.position - transform.position).magnitude;

        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;

        GameObject a = Instantiate(new GameObject());
        a.layer = 15;
        a.transform.parent = transform;
        lineRenderer = a.AddComponent<LineRenderer>();
        lineRenderer.SetWidth(0.5f, 0.5f); //thickness of line
        lineRenderer.SetVertexCount(size);
        lineRenderer.material = mat;

        DrawCircle();
    }

    void DrawCircle()
    {
        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            x += centerPoint.transform.position.x;
            y += centerPoint.transform.position.y;
            pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }
}