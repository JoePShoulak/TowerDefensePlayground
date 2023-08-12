using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput
{
    // Camera
    private static bool MouseUp(CameraController cam)
    {
        return (Input.mousePosition.y >= Screen.height - cam.panBorderThickeness && cam.mouseToMove);
    }

    private static bool MouseDown(CameraController cam)
    {
        return (Input.mousePosition.y <= cam.panBorderThickeness && cam.mouseToMove);
    }

    private static bool MouseLeft(CameraController cam)
    {
        return (Input.mousePosition.x <= cam.panBorderThickeness && cam.mouseToMove);
    }

    private static bool MouseRight(CameraController cam)
    {
        return (Input.mousePosition.x >= Screen.width - cam.panBorderThickeness && cam.mouseToMove);
    }


    public static bool Up(CameraController cam)
    {
        return (Input.GetKey("w") || MouseUp(cam));
    }

    public static bool Down(CameraController cam)
    {
        return (Input.GetKey("s") || MouseDown(cam));
    }

    public static bool Left(CameraController cam)
    {
        return (Input.GetKey("a") || MouseLeft(cam));
    }

    public static bool Right(CameraController cam)
    {
        return (Input.GetKey("d") || MouseRight(cam));
    }

    // Pause
    public static bool Pause()
    {
        return (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P));
    }
}
