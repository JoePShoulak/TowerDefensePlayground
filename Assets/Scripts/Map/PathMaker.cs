using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathType { Waypoint, Start, End, Normal };
public enum Direction { Horizontal, Vertical, Null };

public class PathSection
{
    public GameObject node;
    public PathType type = PathType.Normal;

    public PathSection(GameObject _node, PathType _type)
    {
        node = _node;
        type = _type;
    }
}

public class PathMaker
{
    public static void ColorNode(GameObject node, Color color)
    {
        Renderer renderer = node.GetComponent<Renderer>();

        Material tempMaterial = new Material(renderer.sharedMaterial);
        tempMaterial.color = color;

        renderer.sharedMaterial = tempMaterial;
    }

    static Direction GetDirection(Vector3 posA, Vector3 posB)
    {
        if (posA.x == posB.x) return Direction.Vertical;
        else if (posA.z == posB.z) return Direction.Horizontal;
        else return Direction.Null;
    }

    public static List<PathSection> GetPathFromNodes(List<GameObject> nodes)
    {
        List<PathSection> pathSections = new List<PathSection>();

        Direction dir = Direction.Null;
        Direction oldDir = Direction.Null;

        GameObject currentNode;
        GameObject oldNode = null;
        Vector3 currPos = nodes[0].transform.position;

        for (int i = 0; i < nodes.Count; i++)
        {
            currentNode = nodes[i];

            if (i == 0) pathSections.Add(new PathSection(currentNode, PathType.Start));
            else
            {
                if (i == nodes.Count - 1) pathSections.Add(new PathSection(currentNode, PathType.End));

                dir = GetDirection(currentNode.transform.position, currPos);

                if (dir == Direction.Null) return new List<PathSection>();

                if (dir != oldDir && oldNode != null)
                {
                    pathSections.RemoveAt(pathSections.Count - 1);
                    pathSections.Add(new PathSection(oldNode, PathType.Waypoint));
                    pathSections.Add(new PathSection(currentNode, PathType.Normal));
                }
                else pathSections.Add(new PathSection(currentNode, PathType.Normal));

                currPos = nodes[i].transform.position;
                oldDir = dir;
                oldNode = currentNode;
            }

        }

        return pathSections;
    }
}
