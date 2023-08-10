using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TurretType { StandardTurret, MissileLauncher };

public class TurretCostUI : MonoBehaviour
{
    public TurretType type = TurretType.StandardTurret;
    public TMP_Text costText;
    void Start()
    {
        switch (type)
        {
            case TurretType.StandardTurret:
                // costText.text = Shop.standardTurret.cost;
                break;
            case TurretType.MissileLauncher:
                // costText.text = Shop.missileLauncher.cost;
                break;
            default:
                // costText.text = Shop.standardTurret.cost;
                break;
        }
    }
}
