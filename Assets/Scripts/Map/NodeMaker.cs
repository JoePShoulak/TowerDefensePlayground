using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMaker
{
    public static Vector3 GetSpawnStartLocation(Transform root, int nodesPerRow, float nodeWidth, float spacing)
    {
        float offset = (nodeWidth + spacing) * (nodesPerRow - 1) / 2;
        return root.position + new Vector3(offset, 0, -offset);
    }

    public static GameObject ResizeNode(GameObject _node, float _width, int _nodesPerRow, float _spacing)
    {
        float startingNodeWidth = _node.transform.localScale.x;
        float totalSpacing = (_nodesPerRow - 1) * _spacing;
        float totalNodeWidth = _width - totalSpacing;

        float nodeWidth = totalNodeWidth / _nodesPerRow;
        GameObject nodeCopy = _node;
        nodeCopy.transform.localScale *= nodeWidth / startingNodeWidth;

        return nodeCopy;
    }

    public static List<Vector3> CreateGrid(int objPerRow, float objWidth, float _spacing, Vector3 startPos)
    {
        List<Vector3> grid = new List<Vector3>();

        Vector3 spawnPos = startPos;
        for (int x = 0; x < objPerRow; x++)
        {
            for (int y = 0; y < objPerRow; y++)
            {
                grid.Add(spawnPos);

                if (y == objPerRow - 1) break;
                spawnPos += Vector3.left * (objWidth + _spacing);
            }

            if (x == objPerRow - 1) break;
            spawnPos = startPos + Vector3.forward * (objWidth + _spacing) * (x + 1);
        }

        return grid;
    }

}
