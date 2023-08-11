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

    public GameObject buildEffect;
    [HideInInspector]
    public Node lastHoveredNode;
    public TurretUI turretUI;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public TurretBlueprint TurretToBuild
    {
        get { return turretToBuild; }
        set
        {
            turretToBuild = value;
            DeselectNode();
        }
    }

    public Node SelectedNode { get { return selectedNode; } }

    public void SetSelectedNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretUI.Show(node);
        turretToBuild = null;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        turretUI.Hide();
        ResetLastHoveredNode();

    }

    public bool CanAffordTurret { get { return Player.Money >= turretToBuild.cost; } }



    public void ResetLastHoveredNode()
    {
        if (lastHoveredNode != null) lastHoveredNode.ResetRenderer();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            turretToBuild = null;
            selectedNode = null;
            turretUI.Hide();
            DeselectNode();
        }
    }
}
