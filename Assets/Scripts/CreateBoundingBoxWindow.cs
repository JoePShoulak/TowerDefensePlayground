using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateBoundingBoxWindow : EditorWindow
{
    [MenuItem("Window/CreateBoundingBox")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CreateBoundingBoxWindow>("Create Bounding Box");
    }


    void OnGUI()
    {
        GUILayout.Label("Bounding Box", EditorStyles.boldLabel);
        GUILayout.Label("Can create a box that extends between two square\n(from the top or bottom) boxes of the same dimensions\nthat are inline with each other.\n\nThis also kind of assumes no rotation. \n\nIt's of limited use. ");
        if (GUILayout.Button("Create Bounding Box")) CreateBoundingBox();
    }

    public void CreateBoundingBox()
    {
        List<GameObject> selection = new List<GameObject>(Selection.gameObjects);

        if (!ValidateSelection(selection)) return;

        Transform boxA = selection[0].transform;
        Transform boxB = selection[1].transform;
        Vector3 delta = boxA.transform.position - boxB.transform.position;

        // Well, I guess we're picking X scale...
        float totalWidth = boxA.transform.localScale.x + delta.magnitude;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = boxA.transform.position - delta / 2;

        if (delta.x != 0) cube.transform.localScale = new Vector3(totalWidth, boxA.localScale.y, boxA.localScale.z);
        else if (delta.z != 0) cube.transform.localScale = new Vector3(boxA.localScale.x, boxA.localScale.y, totalWidth);
    }

    bool ValidateSelection(List<GameObject> selection)
    {
        // Must be 2 boxes
        if (selection.Count != 2)
        {
            Debug.LogError("Must be two objects selected");
            return false;
        }

        GameObject boxA = selection[0];
        GameObject boxB = selection[1];

        // Must be the same size
        if (boxA.transform.localScale != boxB.transform.localScale)
        {
            Debug.LogError("Must be the same size");
            return false;
        }

        // Must be inline
        Vector3 delta = boxA.transform.position - boxB.transform.position;
        int dimLock = 0;

        if (delta.x == 0) dimLock++;
        if (delta.y == 0) dimLock++;
        if (delta.z == 0) dimLock++;

        if (dimLock != 2)
        {
            Debug.LogError("Must be inline");
            return false;
        }

        Debug.Log("Valid!!");

        return true;
    }
}
