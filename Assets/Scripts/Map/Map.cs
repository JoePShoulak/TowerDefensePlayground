using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public GameObject nodeObj;
    public GameObject startObj;
    public GameObject endObj;
    public GameObject waypointObj;

    public float width = 70f;
    public int nodesPerRow = 16;
    public float spacing = 0.25f;

    private GameObject nodes;
    private GameObject path;
    private GameObject waypoints;

    public bool NodesExist { get { return nodes != null; } }
    public bool PathExists { get { return path != null; } }

    /* == HELPERS == */
    GameObject MakeEmptyChild(GameObject parent, string name = "New Child")
    {
        GameObject newChild = new GameObject(name);
        newChild.transform.SetParent(parent != null ? parent.transform : transform);
        return newChild;
    }

    /* == NODES == */
    public void MakeNodes()
    {
        if (NodesExist) ClearNodes();
        nodes = MakeEmptyChild(gameObject, "Nodes");
        GameObject resizedNode = NodeMaker.ResizeNode(nodeObj, width, nodesPerRow, spacing);

        float nodeWidth = resizedNode.transform.localScale.x;
        Vector3 spawnCorner = NodeMaker.GetSpawnStartLocation(transform, nodesPerRow, nodeWidth, spacing);

        List<Vector3> spawnLocations = NodeMaker.CreateGrid(nodesPerRow, nodeWidth, spacing, spawnCorner);
        foreach (Vector3 spawnLocation in spawnLocations)
        {
            GameObject newNode = (GameObject)Instantiate(nodeObj, spawnLocation, Quaternion.identity, nodes.transform);
        }
    }

    public void ClearNodes()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }

    /* == PATHS == */
    public void MakePath(List<GameObject> selectedNodes)
    {
        if (selectedNodes.Count < 3)
        {
            Debug.Log("Path too short");
            return;
        }

        List<PathSection> pathSections = PathMaker.GetPathFromNodes(selectedNodes);

        if (pathSections.Count == 0)
        {
            Debug.LogError("Path has a continuity issue");
            return;
        }

        if (path == null) path = MakeEmptyChild(gameObject, "Path");
        SpawnPathFromSections(pathSections);
    }

    public void ResetPath()
    {
        DestroyPath();
        MakeNodes();
    }

    void DestroyPath()
    {
        DestroyImmediate(path);
    }

    void SpawnPathFromSections(List<PathSection> sections)
    {
        waypoints = MakeEmptyChild(path, "Waypoints");
        waypoints.AddComponent<Waypoints>();
        foreach (PathSection section in sections)
        {
            Vector3 offset = Vector3.up * (nodeObj.transform.localScale.y + startObj.transform.localScale.y) / 2;
            section.node.transform.SetParent(path.transform);

            switch (section.type)
            {
                case PathType.Waypoint:
                    Instantiate(waypointObj, section.node.transform.position + offset, Quaternion.identity, waypoints.transform);
                    PathMaker.ColorNode(section.node, Color.gray);
                    break;
                case PathType.Normal:
                    PathMaker.ColorNode(section.node, Color.gray);
                    break;
                case PathType.Start:
                    Instantiate(startObj, section.node.transform.position + offset, Quaternion.identity, path.transform);
                    break;
                case PathType.End:
                    Instantiate(waypointObj, section.node.transform.position + offset, Quaternion.identity, waypoints.transform);
                    Instantiate(endObj, section.node.transform.position + offset, Quaternion.identity, path.transform);
                    break;
                default:
                    break;
            }
        }
    }

    void OnValidate()
    {
        Debug.Log(PathExists);
    }
}

/* == WAYPOINTS == */
