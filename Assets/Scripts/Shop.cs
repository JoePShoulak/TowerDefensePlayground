using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.TurretToBuild = standardTurret;
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.TurretToBuild = missileLauncher;
    }

    public void OnValidate()
    {
        standardTurret.cost = Mathf.RoundToInt(Mathf.Max(0f, (float)standardTurret.cost));
        missileLauncher.cost = Mathf.RoundToInt(Mathf.Max(0f, (float)missileLauncher.cost));
    }
}
