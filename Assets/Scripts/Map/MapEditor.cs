using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Map script = (Map)target;

        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear All")) script.ClearAll();
        if (GUILayout.Button("Make Nodes")) script.MakeNodes();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Path")) script.ClearPath();
        EditorGUI.BeginDisabledGroup(script.PathExists || !script.NodesExist);
        if (GUILayout.Button("Make Path")) script.MakePath(new List<GameObject>(Selection.gameObjects));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }
}
