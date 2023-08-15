using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color canBuildColor = Color.green;
    public Color cannotBuildColor = Color.red;

    public Vector3 turretOffset;
    public Vector3 effectOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint blueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startingColor;
    private GameObject rangePreview;
    private float scaleFactor = 1f;

    BuildManager buildManager;

    public Vector3 BuildPosition { get { return transform.position + turretOffset; } }
    public Vector3 EffectPosition { get { return transform.position + effectOffset; } }

    public void SetNodeToBuildabilityColor()
    {

        if (turret == null && buildManager.CanAffordTurret) rend.material.color = canBuildColor;
        else rend.material.color = cannotBuildColor;
    }

    public void ShowRangePreview()
    {
        if (rangePreview != null) return;

        TurretBlueprint turrBlueprint;

        if (buildManager.TurretToBuild != null) turrBlueprint = buildManager.TurretToBuild;
        else turrBlueprint = blueprint;

        GameObject gfx = turrBlueprint.rangeGfx;
        float range = turrBlueprint.prefab.GetComponent<Turret>().range * scaleFactor;

        rangePreview = (GameObject)Instantiate(gfx, BuildPosition, Quaternion.identity);
        rangePreview.transform.localScale = Vector3.one * range;
    }

    public void PreviewTurret()
    {
        SetNodeToBuildabilityColor();

        ShowRangePreview();
    }



    void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject() || buildManager.TurretToBuild == null) return;


        buildManager.lastHoveredNode = this;

        if (buildManager.TurretToBuild != null) PreviewTurret();

    }

    public void ResetNode()
    {
        rend.material.color = startingColor;
        if (rangePreview != null) Destroy(rangePreview);
    }

    void OnMouseExit()
    {
        if (buildManager.SelectedNode == this) return;

        ResetNode();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null) buildManager.SetSelectedNode(this);
        else if (buildManager.TurretToBuild != null)
        {
            BuildTurret();
            ResetNode();
        }

    }

    void PlaceTurret(bool upgrading = false)
    {
        if (!upgrading) blueprint = buildManager.TurretToBuild;
        int cost = upgrading ? blueprint.upgradeCost : blueprint.cost;

        if (Player.Money < cost) return;

        blueprint.currentSellPrice = upgrading ? blueprint.upgradedSellPrice : blueprint.sellPrice;
        GameObject prefab = upgrading ? blueprint.upgradedPrefab : blueprint.prefab;

        if (upgrading) Destroy(turret);
        Player.Money -= cost;
        isUpgraded = upgrading;
        EffectManager.Spawn(2f, blueprint.buildEffect, EffectPosition);
        turret = (GameObject)Instantiate(prefab, BuildPosition, Quaternion.identity);
        turret.transform.localScale *= scaleFactor;
        turret.GetComponent<Turret>().range *= scaleFactor;

        buildManager.TurretToBuild = null;

        Debug.Log("Turret " + (upgrading ? "Upgraded" : "Placed"));
    }

    void BuildTurret() { PlaceTurret(); }

    public void UpgradeTurret() { PlaceTurret(true); }

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

        EffectManager.Spawn(2f, blueprint.sellEffect, EffectPosition);
        Clear();
    }

    // Main Stuff
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
        scaleFactor = transform.localScale.y;

    }
}
