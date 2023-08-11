using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TurretType { StandardTurret, MissileLauncher, LaserBeamer };

public class TurretButtonUI : MonoBehaviour
{
    public TurretType turret;
    public TMP_Text costText;

    void Start()
    {
        switch (turret)
        {
            case TurretType.StandardTurret:
                costText.text = "$" + Shop.StandardTurret.cost;
                break;
            case TurretType.MissileLauncher:
                costText.text = "$" + Shop.MissileLauncher.cost;
                break;
            case TurretType.LaserBeamer:
                costText.text = "$" + Shop.LaserBeamer.cost;
                break;
            default:
                break;
        }
    }
}
