using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.green;
    public Color occupiedHoverColor = Color.red;

    public Vector3 turretOffset;

    private Renderer rend;
    private Color startingColor;
    private GameObject turret;

    BuildManager buildManager;

    // Mouse Stuff
    void OnMouseOver()
    {
        buildManager.lastHoveredNode = this;

        if (EventSystem.current.IsPointerOverGameObject() || buildManager.GetTurretToBuild() == null)
            return;

        if (turret == null)
            rend.material.color = hoverColor;
        else
            rend.material.color = occupiedHoverColor;
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
        if (turret != null)
            return;

        GameObject turretToBuild = buildManager.GetTurretToBuild();
        if (turretToBuild == null)
            return;

        BuildTurret(turretToBuild);
    }

    // Fun Stuff
    void BuildTurret(GameObject _turret)
    {
        Debug.Log("Turret Placed");
        turret = (GameObject)Instantiate(_turret, transform.position + turretOffset, transform.rotation);
        buildManager.SetTurretToBuild(null);
    }

    // Main Stuff
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }
}
