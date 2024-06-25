# Unity Magic Mouse Panning Script

This Unity editor script enables panning in the scene view on macOS using a Magic Mouse. By leveraging the [MiddleClick](https://github.com/artginzburg/MiddleClick-Sonoma) app, this script emulates middle mouse button functionality, allowing for seamless navigation in the Unity Editor. The original app doesn't work with unity for panning, it only allows you to pan if you hold down the option key and the three finger click and hold, which is too much of a hassle.

The script allows for panning that feels exactly like panning natively in Unity with a mouse.

## Requirements

- **MacOS Sonoma (Tested on Sonoma 14.5)**
- **Unity Editor**
- **Magic Mouse**
- **MiddleClick App**: [Download Here](https://github.com/artginzburg/MiddleClick-Sonoma/releases/tag/2.7)

## Installation

1. **Download and Install MiddleClick**

2. **Download the Editor Script**: [Raw CS file on Github] (https://raw.githubusercontent.com/danieltsoftware/UnityPanningMagicMouse/main/SceneViewPanner.cs)

3. **Add the Script to Your Unity Project**:
   - Open your Unity project.
   - Create an `Editor` folder in your project's `Assets` directory if it doesn't already exist.
   - Copy the `SceneViewPanner.cs` script into the `Editor` folder.

4. **Try it!** (You might need to click inside the scene view first to activate the script, sometimes the scene view becomes unfocused)

## Usage

Once installed, the script allows you to pan in the Unity scene view using the Magic Mouse with the middle mouse button emulation provided by the MiddleClick app.

### How It Works

The script listens for middle mouse button events and moves the camera when the middle mouse button is pressed and dragged using mouse movement delta. The panSpeed 0.004f feels like one to one with the mouse which is what I was going for.

## Script Details

```
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SceneViewPanner
{
    static SceneViewPanner()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static Vector3 lastMousePosition;
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
        else if (e.type == EventType.MouseDrag && isPanning)
        {
            Vector3 delta = e.mousePosition - (Vector2)lastMousePosition;
            delta.y = -delta.y;
            PanSceneView(sceneView, delta);
            lastMousePosition = e.mousePosition;
            e.Use();
        }
    }

    static void PanSceneView(SceneView sceneView, Vector3 delta)
    {
        Camera cam = sceneView.camera;
        
        // Adjust the panning speed
        float panSpeed = 0.004f;

        if (cam.orthographic)
        {
            // Orthographic mode
            Vector3 movement = cam.transform.right * -delta.x + cam.transform.up * -delta.y;
            sceneView.pivot += movement * panSpeed * cam.orthographicSize;
        }
        else
        {
            // Perspective mode, !README: feel free to remove the distance calculation if that works better for you
            float distance = Vector3.Distance(sceneView.pivot, cam.transform.position);
            Vector3 movement = cam.transform.right * -delta.x + cam.transform.up * -delta.y;
            sceneView.pivot += movement * panSpeed * distance;
        }

        sceneView.Repaint();
    }
}
```

## Contributing

Feel free to submit issues or pull requests if you find any bugs or have suggestions for improvements.

## License

This project is licensed under the Apache 2.0 License - see the [LICENSE](LICENSE) file for details.

## Huge thank you

[MiddleClick App](https://github.com/artginzburg/MiddleClick-Sonoma) by Arthur Ginzburg
