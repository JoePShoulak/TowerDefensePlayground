using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject nodeObj;
    public GameObject startObj;
    public GameObject endObj;
    public GameObject waypointObj;

    public Material roadMaterial;

    public float width = 70f;
    public int nodesPerRow = 16;
    public float spacing = 0.25f;

    private GameObject nodes;
    private GameObject path;
    private GameObject waypoints;
    private GameObject road;

    public bool NodesExist
    {
        get
        {
            Transform _nodes = transform.Find("Nodes");
            if (_nodes == null)
            {
                nodes = null;
                return false;
            }

            nodes = _nodes.gameObject;
            return true;

        }
    }

    public bool PathExists
    {
        get
        {
            Transform _path = transform.Find("Path");
            if (_path == null)
            {
                path = null;
                return false;
            }

            path = _path.gameObject;
            return true;
        }
    }


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
        if (NodesExist) ClearAll();

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

    public void ClearAll()
    {
        if (nodes != null) DestroyImmediate(nodes);
        if (path != null) DestroyImmediate(path);
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

    public void ClearPath()
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
        road = MakeEmptyChild(path, "Road");
        waypoints.AddComponent<Waypoints>();

        GameObject roadStart = sections[0].node;

        for (int i = 0; i < sections.Count - 1; i++)
        {
            PathSection section = sections[i];
            SpawnSinglePathFromSection(section, waypoints);
            if (i == 0) continue;

            if (section.type != PathType.Normal)
            {
                CreateBoundingBox(roadStart, section.node, road);
                roadStart = section.node;
                if (section.type == PathType.End) DestroyImmediate(section.node);
            }

        }

        foreach (PathSection section in sections)
        {
            DestroyImmediate(section.node);
        }
    }

    void SpawnSinglePathFromSection(PathSection section, GameObject parent)
    {
        section.node.transform.SetParent(path.transform);
        Vector3 offset = Vector3.up * (section.node.transform.localScale.y + startObj.transform.localScale.y) / 2;
        Vector3 pos = section.node.transform.position + offset;

        switch (section.type)
        {
            case PathType.Waypoint:
                Instantiate(waypointObj, pos, Quaternion.identity, parent.transform);
                break;
            case PathType.Normal:
                break;
            case PathType.Start:
                Instantiate(startObj, pos, Quaternion.identity, parent.transform);
                break;
            case PathType.End:
                Instantiate(waypointObj, pos, Quaternion.identity, parent.transform);
                Instantiate(endObj, pos, Quaternion.identity, parent.transform);
                break;
            default:
                break;
        }
    }

    /* == ROADS == */


    public void CreateBoundingBox(GameObject boxA, GameObject boxB, GameObject parent = null)
    {
        Vector3 delta = boxA.transform.position - boxB.transform.position;

        // Well, I guess we're picking X scale...
        float totalWidth = boxA.transform.localScale.x + delta.magnitude;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = boxA.transform.position - delta / 2;
        cube.GetComponent<Renderer>().material = roadMaterial;
        if (parent != null) cube.transform.SetParent(parent.transform);

        if (delta.x != 0) cube.transform.localScale = new Vector3(totalWidth, boxA.transform.localScale.y, boxA.transform.localScale.z);
        else if (delta.z != 0) cube.transform.localScale = new Vector3(boxA.transform.localScale.x, boxA.transform.localScale.y, totalWidth);
    }
}
