using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TurretType { StandardTurret, MissileLauncher, LaserBeamer };

public class TurretButtonUI : MonoBehaviour
{
    public TurretType turret;
    public TMP_Text costText;
    TurretManager turretManager;


    void Start()
    {
        turretManager = TurretManager.instance;


        switch (turret)
        {
            case TurretType.StandardTurret:
                costText.text = "$" + turretManager.standardTurret.cost;
                break;
            case TurretType.MissileLauncher:
                costText.text = "$" + turretManager.missileLauncher.cost;
                break;
            case TurretType.LaserBeamer:
                costText.text = "$" + turretManager.laserBeamer.cost;
                break;
            default:
                break;
        }
    }
}
