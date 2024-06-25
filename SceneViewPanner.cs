using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SceneViewPanner
{
    static SceneViewPanner()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static Vector2 lastMousePosition = Vector2.zero;
    static bool isPanning = false;
    
    static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 2)
        {
            isPanning = true;
            lastMousePosition = e.mousePosition;
            e.Use();
        }
        else if (e.type == EventType.MouseUp && e.button == 2)
        {
            isPanning = false;
            e.Use();
        }
        
        if (isPanning && Event.current != null)
        {
            Vector2 currentMousePosition = Event.current.mousePosition;
            Vector2 delta = currentMousePosition - lastMousePosition;
            delta.y = -delta.y;
            lastMousePosition = currentMousePosition;

            if (sceneView!= null)
            {
                PanSceneView(sceneView, delta);
            }
        }
    }

    static void PanSceneView(SceneView sceneView, Vector3 delta)
    {
        Camera cam = sceneView.camera;
        float panSpeed = 0.004f;

        if (cam.orthographic)
        {
            // Orthographic mode
            Vector3 movement = cam.transform.right * -delta.x + cam.transform.up * -delta.y;
            sceneView.pivot += movement * panSpeed * cam.orthographicSize;
        }
        else
        {
            // Perspective mode
            float distance = Vector3.Distance(sceneView.pivot, cam.transform.position);
            Vector3 movement = cam.transform.right * -delta.x + cam.transform.up * -delta.y;
            sceneView.pivot += movement * panSpeed * distance;
        }

        sceneView.Repaint();
    }
}
