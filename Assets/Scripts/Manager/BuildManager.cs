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

    private TurretBlueprint turretToBuild;

    public TurretBlueprint TurretToBuild
    {
        get { return turretToBuild; }
        set { turretToBuild = value; }
    }

    // FIXME: Probably shouldn't be in this class
    [HideInInspector]
    public Node lastHoveredNode;
    public void ResetLastHoveredNode()
    {
        if (lastHoveredNode == null) return;
        lastHoveredNode.ResetRenderer();
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost) return;

        PlayerStats.Money -= turretToBuild.cost;
        node.turret = (GameObject)Instantiate(turretToBuild.prefab, node.transform.position + node.turretOffset, Quaternion.identity);
        turretToBuild = null;
        Debug.Log("Turret Placed");
    }
}
