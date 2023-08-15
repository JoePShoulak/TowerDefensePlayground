using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { Horizontal, Vertical, Null };

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

    public MonoBehaviour waypointsScript;

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
        GameObject resizedNode = ResizeNode(nodeObj, width, nodesPerRow, spacing);

        float nodeWidth = resizedNode.transform.localScale.x;
        Vector3 spawnLocation = GetSpawnStartLocation(nodesPerRow, nodeWidth, spacing);

        SpawnPrefabInSquareGrid(resizedNode, nodesPerRow, nodeWidth, spacing, spawnLocation, nodes);
    }

    public void ClearNodes()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }

    Vector3 GetSpawnStartLocation(int nodesPerRow, float nodeWidth, float spacing)
    {
        float offset = (nodeWidth + spacing) * (nodesPerRow - 1) / 2;
        return transform.position + new Vector3(offset, 0, -offset);
    }

    GameObject ResizeNode(GameObject _node, float _width, int _nodesPerRow, float _spacing)
    {
        float startingNodeWidth = _node.transform.localScale.x;
        float totalSpacing = (_nodesPerRow - 1) * _spacing;
        float totalNodeWidth = _width - totalSpacing;

        float nodeWidth = totalNodeWidth / _nodesPerRow;
        GameObject nodeCopy = nodeObj;
        nodeCopy.transform.localScale *= nodeWidth / startingNodeWidth;

        return nodeCopy;
    }

    void SpawnPrefabInSquareGrid(GameObject gObj, int objPerRow, float objWidth, float spacing, Vector3 startPos, GameObject parent = null)
    {
        Vector3 spawnPos = startPos;
        for (int x = 0; x < objPerRow; x++)
        {
            for (int y = 0; y < objPerRow; y++)
            {
                GameObject newNode = (GameObject)Instantiate(nodeObj, spawnPos, Quaternion.identity, parent != null ? parent.transform : transform);

                if (y == objPerRow - 1) break;
                spawnPos += Vector3.left * (objWidth + spacing);
            }

            if (x == objPerRow - 1) break;
            spawnPos = startPos + Vector3.forward * (objWidth + spacing) * (x + 1);
        }
    }

    /* == PATHS == */
    public void MakePath(List<GameObject> selectedNodes)
    {
        if (!ValidatePath(selectedNodes)) return;

        SpawnPath(selectedNodes);

        Debug.Log("Paths finished");
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

    bool ValidatePath(List<GameObject> nodes)
    {
        if (nodes.Count == 0)
        {
            Debug.Log("Nothing selected");
            return false;
        }

        if (path != null) ResetPath();

        path = MakeEmptyChild(gameObject, "Path");

        return true;
    }

    void SpawnPath(List<GameObject> nodes)
    {
        Direction dir = Direction.Null;
        Direction oldDir = Direction.Null;

        GameObject currentNode;
        GameObject oldNode = null;
        Vector3 currPos = nodes[0].transform.position;

        Vector3 offset = Vector3.up * (nodeObj.transform.localScale.y + startObj.transform.localScale.y) / 2;
        waypoints = MakeEmptyChild(path, "Waypoints");
        waypoints.AddComponent<Waypoints>();

        for (int i = 0; i < nodes.Count; i++)
        {
            currentNode = nodes[i];
            currentNode.transform.SetParent(path.transform);

            if (i == 0)
            {
                SpawnSimpleObject(currentNode, startObj, offset);
                continue;
            }
            else if (i == nodes.Count - 1)
            {
                SpawnSimpleObject(currentNode, endObj, offset);
                SpawnWaypoint(currentNode, offset);
            }
            else ColorNode(currentNode, Color.gray);

            dir = GetDirection(currentNode.transform.position, currPos);
            if (dir == Direction.Null)
            {
                Debug.LogError("Path not continuous and/or in order");
                ResetPath();
                return;
            }

            if (dir != oldDir && oldNode != null) SpawnWaypoint(oldNode, offset);

            currPos = nodes[i].transform.position;
            oldDir = dir;
            oldNode = currentNode;
        }
    }

    void SpawnWaypoint(GameObject node, Vector3 offset)
    {
        Debug.Log(node);
        Debug.Log(offset);
        Debug.Log(waypoints);
        Debug.Log(waypointObj);
        Debug.Log("-----------");
        GameObject wp = (GameObject)Instantiate(waypointObj, node.transform.position + offset, Quaternion.identity, waypoints.transform);
    }

    GameObject SpawnSimpleObject(GameObject node, GameObject obj, Vector3 offset)
    {
        GameObject objI = (GameObject)Instantiate(obj, node.transform.position, Quaternion.identity, path.transform);
        objI.transform.position += offset;

        return objI;
    }

    Direction GetDirection(Vector3 posA, Vector3 posB)
    {
        if (posA.x == posB.x) return Direction.Vertical;
        else if (posA.z == posB.z) return Direction.Horizontal;
        else return Direction.Null;
    }

    void ColorNode(GameObject node, Color color)
    {
        Renderer renderer = node.GetComponent<Renderer>();

        Material tempMaterial = new Material(renderer.sharedMaterial);
        tempMaterial.color = color;

        renderer.sharedMaterial = tempMaterial;
    }
}

/* == WAYPOINTS == */
