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
    [HideInInspector]
    public bool isUpgraded = false;

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

    // FIXME Not dry enough
    void BuildTurret()
    {
        blueprint = buildManager.TurretToBuild;

        if (!buildManager.CanAffordTurret) return;

        Player.Money -= blueprint.cost;
        blueprint.currentSellPrice = blueprint.sellPrice;
        EffectManager.Spawn(2f, blueprint.buildEffect, BuildPosition);
        turret = (GameObject)Instantiate(blueprint.prefab, BuildPosition, Quaternion.identity);
        buildManager.TurretToBuild = null;

        Debug.Log("Turret Placed");
    }

    public void UpgradeTurret()
    {
        if (!(Player.Money >= blueprint.upgradeCost) || isUpgraded) return;

        Player.Money -= blueprint.cost;
        isUpgraded = true;
        blueprint.currentSellPrice = blueprint.upgradedSellPrice;
        Destroy(turret);
        EffectManager.Spawn(2f, blueprint.buildEffect, BuildPosition);
        turret = (GameObject)Instantiate(blueprint.upgradedPrefab, BuildPosition, Quaternion.identity);
        buildManager.TurretToBuild = null;

        Debug.Log("Turret Upgraded");
    }

    public void Clear()
    {
        Destroy(turret);
        turret = null;
        blueprint = null;
        isUpgraded = false;
    }

    public void SellTurret()
    {
        if (isUpgraded) Player.Money += blueprint.upgradedSellPrice;
        else Player.Money += blueprint.sellPrice;

        Clear();
    }

    // Main Stuff
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }
}
