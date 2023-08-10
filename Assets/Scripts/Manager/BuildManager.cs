using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern
    public static BuildManager instance;

    public GameObject buildEffect;
    public Vector3 buildEffectOffset;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;

    public TurretBlueprint TurretToBuild
    {
        get { return turretToBuild; }
        set { turretToBuild = value; }
    }

    public bool CanAffordTurret { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (!CanAffordTurret) return;

        PlayerStats.Money -= turretToBuild.cost;
        EffectManager.Spawn(2f, buildEffect, node.BuildPosition + buildEffectOffset);
        node.turret = (GameObject)Instantiate(turretToBuild.prefab, node.BuildPosition, Quaternion.identity);
        turretToBuild = null;
        Debug.Log("Turret Placed");
    }

    // FIXME: Probably shouldn't be in this class
    [HideInInspector]
    public Node lastHoveredNode;
    public void ResetLastHoveredNode()
    {
        if (lastHoveredNode != null) lastHoveredNode.ResetRenderer();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            turretToBuild = null;
            ResetLastHoveredNode();
        }
    }
}
