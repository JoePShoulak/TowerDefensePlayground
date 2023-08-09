using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    // FIXME: Probably shouldn't be in this class
    public Node lastHoveredNode;
    public void ResetLastHoveredNode()
    {
        lastHoveredNode.ResetRenderer();
    }
}
