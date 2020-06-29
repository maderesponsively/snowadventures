using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{


    private Camera cam;

    private LineRenderer lineRenderer;
    private float width = 1.0f;
    private new Renderer renderer;


    private void Awake()
    {
        

    }

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //AnimationCurve curve = new AnimationCurve();
        //if (useCurve)
        //{
        //    curve.AddKey(0.0f, 0.0f);
        //    curve.AddKey(1.0f, 1.0f);
        //}
        //else
        //{
        //    curve.AddKey(0.0f, 1.0f);
        //    curve.AddKey(1.0f, 1.0f);
        //}

        //lineRenderer.widthCurve = curve;
        //lineRenderer.widthMultiplier = width;
    }

    void OnGUI()
    {
        Event m_Event = Event.current;

        cam = Camera.main;

        //Vector3 point = new Vector3();
 
        Vector2 mousePos = new Vector2();

        mousePos.x = m_Event.mousePosition.x;
        mousePos.y = cam.pixelHeight - m_Event.mousePosition.y;

        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        point.z = 0;
        
        if (m_Event.type == EventType.MouseDown)
        {

            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.sortingLayerName = "Foreground";
            lineRenderer.sortingOrder = 3;
            //lineRenderer.useWorldSpace = true;
            //lineRenderer.widthMultiplier = 0.1f;

            lineRenderer.startWidth = 0f;
            lineRenderer.endWidth = 0.2f;


        }

        if (m_Event.type == EventType.MouseDrag)
        {

            Vector3 playerPoint = new Vector3(renderer.bounds.center.x, renderer.bounds.center.y - 0.3f, renderer.bounds.center.z);


            Vector3[] positions = new Vector3[2];
            positions[0] = playerPoint;
            positions[1] = point;


            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);

            //UpdateLine(positions);
        }

        if (m_Event.type == EventType.MouseUp)
        {
            Destroy(lineRenderer);
        }
    }

    void UpdateLine(Vector3[] positions)
    {
   
    }
}
