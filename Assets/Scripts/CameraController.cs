using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float scrollSpeed = 10f;
    public float panBorderThickeness = 10f;
    public bool mouseToMove = false;
    public float minY = 10f;
    public float maxY = 80f;

    void Update()
    {
        if (GameManager.GameEnded) return;

        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickeness && mouseToMove))
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickeness && mouseToMove))
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickeness && mouseToMove))
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickeness && mouseToMove))
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
