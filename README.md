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

2. **Download the Editor Script**: [Raw CS file on Github](https://raw.githubusercontent.com/danieltsoftware/UnityPanningMagicMouse/main/SceneViewPanner.cs)

3. **Add the Script to Your Unity Project**:
   - Open your Unity project.
   - Create an `Editor` folder in your project's `Assets` directory if it doesn't already exist.
   - Copy the `SceneViewPanner.cs` script into the `Editor` folder.

4. **Success!**

## Usage

Once installed, the script allows you to pan in the Unity scene view using the Magic Mouse with the middle mouse button emulation provided by the MiddleClick app.

### How It Works

The script listens for middle mouse button events and moves the camera when the middle mouse button is pressed and dragged using mouse movement delta. The panSpeed 0.004f feels like one to one with the mouse which is what I was going for.

## Contributing

Feel free to submit issues or pull requests if you find any bugs or have suggestions for improvements.

## License

This project is licensed under the Apache 2.0 License - see the [LICENSE](LICENSE) file for details.

## Huge thank you

[MiddleClick App](https://github.com/artginzburg/MiddleClick-Sonoma) by Arthur Ginzburg
