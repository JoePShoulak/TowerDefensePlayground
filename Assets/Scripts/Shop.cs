using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    TurretManager turretManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        turretManager = TurretManager.instance;
    }

    public void Select(TurretBlueprint turret)
    {
        buildManager.TurretToBuild = turret;
        Debug.Log(turret.prefab.name + " Selected");
    }

    public void SelectStandardTurret() { Select(turretManager.standardTurret); }

    public void SelectMissileLauncher() { Select(turretManager.missileLauncher); }

    public void SelectLaserBeamer() { Select(turretManager.laserBeamer); }
}
