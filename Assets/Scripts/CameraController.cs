using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput
{
    public static bool Up(CameraController cam)
    {
        return (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - cam.panBorderThickeness && cam.mouseToMove));
    }

    public static bool Down(CameraController cam)
    {
        return (Input.GetKey("s") || (Input.mousePosition.y <= cam.panBorderThickeness && cam.mouseToMove));
    }

    public static bool Left(CameraController cam)
    {
        return (Input.GetKey("a") || (Input.mousePosition.x <= cam.panBorderThickeness && cam.mouseToMove));
    }

    public static bool Right(CameraController cam)
    {
        return (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - cam.panBorderThickeness && cam.mouseToMove));
    }
}

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float scrollSpeed = 10f;
    public float panBorderThickeness = 10f;
    public bool mouseToMove = false;
    public float minY = 10f;
    public float maxY = 80f;

    void Move(Vector3 dir)
    {
        transform.Translate(dir * panSpeed * Time.deltaTime, Space.World);
    }

    void Update()
    {
        if (GameManager.GameEnded) return;

        if (GetInput.Up(this)) Move(Vector3.forward);
        if (GetInput.Down(this)) Move(Vector3.back);
        if (GetInput.Left(this)) Move(Vector3.left);
        if (GetInput.Right(this)) Move(Vector3.right);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
