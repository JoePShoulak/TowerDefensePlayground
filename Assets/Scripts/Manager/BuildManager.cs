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
    public GameObject missileTurretPrefab;
    private GameObject turretToBuild;

    public GameObject TurretToBuild
    {
        get { return turretToBuild; }
        set { turretToBuild = value; }
    }

    // FIXME: Probably shouldn't be in this class
    public Node lastHoveredNode;
    public void ResetLastHoveredNode()
    {
        if (lastHoveredNode == null) return;
        lastHoveredNode.ResetRenderer();
    }
}
