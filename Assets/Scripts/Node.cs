using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color canBuildColor = Color.green;
    public Color cannotBuildColor = Color.red;

    public Vector3 turretOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint blueprint;

    private Renderer rend;
    private Color startingColor;

    BuildManager buildManager;

    public Vector3 BuildPosition { get { return transform.position + turretOffset; } }

    // Mouse Stuff
    void OnMouseOver()
    {
        buildManager.lastHoveredNode = this;

        if (EventSystem.current.IsPointerOverGameObject() || buildManager.TurretToBuild == null) return;

        if (turret == null && buildManager.CanAffordTurret) rend.material.color = canBuildColor;
        else rend.material.color = cannotBuildColor;
    }

    public void ResetRenderer()
    {
        rend.material.color = startingColor;
    }

    void OnMouseExit()
    {
        ResetRenderer();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            buildManager.SetSelectedNode(this);
        }
        else if (buildManager.TurretToBuild != null)
        {
            BuildTurret();
        }

        ResetRenderer();
    }

    void BuildTurret()
    {
        blueprint = buildManager.TurretToBuild;

        if (!(Player.Money >= blueprint.cost)) return;

        Player.Money -= blueprint.cost;
        EffectManager.Spawn(2f, blueprint.buildEffect, BuildPosition);
        turret = (GameObject)Instantiate(blueprint.prefab, BuildPosition, Quaternion.identity);
        buildManager.TurretToBuild = null;

        Debug.Log("Turret Placed");
    }

    public void UpgradeTurret()
    {
        if (!(Player.Money >= blueprint.upgradeCost)) return;

        Player.Money -= blueprint.cost;
        EffectManager.Spawn(2f, blueprint.buildEffect, BuildPosition);
        turret = (GameObject)Instantiate(blueprint.prefab, BuildPosition, Quaternion.identity);
        buildManager.TurretToBuild = null;

        Debug.Log("Turret Placed");
    }

    // Main Stuff
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }
}
