using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapMaker))]
public class MapMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapMaker script = (MapMaker)target;

        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Nodes")) script.ClearNodes();
        if (GUILayout.Button("Make Nodes")) script.MakeNodes();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Path")) script.ClearPath();
        EditorGUI.BeginDisabledGroup(script.PathExists);
        if (GUILayout.Button("Make Path")) script.MakePath(new List<GameObject>(Selection.gameObjects));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }
}
