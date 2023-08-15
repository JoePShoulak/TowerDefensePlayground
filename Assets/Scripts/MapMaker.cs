using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { Horizontal, Vertical, Null };

public class MapMaker : MonoBehaviour
{
    public GameObject node;
    public GameObject start;
    public GameObject end;

    public float width = 70f;
    public int nodesPerRow = 16;
    public float spacing = 0.25f;

    private Vector3 spawnLocation;
    private float nodeWidth;

    private GameObject nodes;
    private GameObject path;

    public bool PathExists { get { return path != null; } }

    /* == NODES == */
    public void MakeNodes()
    {
        ClearNodes();
        MakeNodesParent();

        GetSpawnStartLocation();
        SpawnNodes();
    }

    public void ClearNodes()
    {
        DestroyImmediate(nodes);
    }

    void GetSpawnStartLocation()
    {
        ResizeNodePrefab();

        float offset = (nodeWidth + spacing) * (nodesPerRow - 1) / 2;
        spawnLocation = transform.position + new Vector3(offset, 0, -offset);
    }

    void ResizeNodePrefab()
    {
        float startingNodeWidth = node.transform.localScale.x;
        float totalSpacing = (nodesPerRow - 1) * spacing;
        float totalNodeWidth = width - totalSpacing;

        nodeWidth = totalNodeWidth / nodesPerRow;
        node.transform.localScale *= nodeWidth / startingNodeWidth;
    }

    void MakeNodesParent()
    {
        nodes = new GameObject("Nodes");
        nodes.transform.SetParent(transform);
    }

    void SpawnNodes()
    {
        Vector3 startPos = spawnLocation;
        for (int x = 0; x < nodesPerRow; x++)
        {
            for (int y = 0; y < nodesPerRow; y++)
            {
                SpawnNode(spawnLocation);

                if (y == nodesPerRow - 1) break;
                spawnLocation += Vector3.left * (nodeWidth + spacing);
            }

            if (x == nodesPerRow - 1) break;
            spawnLocation = startPos + Vector3.forward * (nodeWidth + spacing) * (x + 1);
        }
    }

    void SpawnNode(Vector3 location)
    {
        GameObject newNode = (GameObject)Instantiate(node, location, Quaternion.identity, nodes.transform);
    }

    /* == PATHS == */
    public void MakePath(List<GameObject> selectedNodes)
    {
        if (!ValidatePath(selectedNodes)) return;

        SpawnPath(selectedNodes);
    }

    public void ClearPath()
    {
        MakeNodes();
        DestroyImmediate(path);
    }

    bool ValidatePath(List<GameObject> nodes)
    {
        if (nodes.Count == 0)
        {
            Debug.Log("Nothing selected");
            return false;
        }

        if (path != null) ClearPath();
        MakePathParent();
        return true;
    }

    void SpawnPath(List<GameObject> nodes)
    {
        Direction dir = Direction.Null;
        Direction oldDir = Direction.Null;

        GameObject node;
        GameObject oldNode = null;
        Vector3 currPos = nodes[0].transform.position;

        for (int i = 0; i < nodes.Count; i++)
        {
            node = nodes[i];
            node.transform.SetParent(path.transform);

            if (i == 0)
            {
                SpawnStart(node);
                continue;
            }
            else if (i == nodes.Count - 1) SpawnEnd(node);
            else ColorNode(node, Color.gray);

            dir = GetDirection(node.transform.position, currPos);
            if (dir == Direction.Null)
            {
                Debug.LogError("Path not continuous and/or in order");
                ClearPath();
                return;
            }

            if (dir != oldDir && oldNode != null) SpawnWaypoint(oldNode);

            currPos = nodes[i].transform.position;
            oldDir = dir;
            oldNode = node;
        }
    }

    void SpawnPathCap(GameObject node, GameObject cap)
    {
        GameObject capGO = (GameObject)Instantiate(cap, node.transform.position, Quaternion.identity, path.transform);
        capGO.transform.position += Vector3.up * (node.transform.localScale.y + capGO.transform.localScale.y) / 2;
    }

    void SpawnStart(GameObject node)
    {
        SpawnPathCap(node, start);
    }

    void SpawnEnd(GameObject node)
    {
        SpawnPathCap(node, end);
    }

    void SpawnWaypoint(GameObject node)
    {
        ColorNode(node, Color.blue);
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

    void MakePathParent()
    {
        path = new GameObject("Path");
        path.transform.SetParent(transform);
    }
}
