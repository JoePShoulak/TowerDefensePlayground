using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color canBuildColor = Color.green;
    public Color cannotBuildColor = Color.red;

    public Vector3 turretOffset;

    [Header("Optional")]
    public GameObject turret;

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
        if (turret != null || buildManager.TurretToBuild == null) return;

        buildManager.BuildTurretOn(this);
    }

    // Main Stuff
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }
}
