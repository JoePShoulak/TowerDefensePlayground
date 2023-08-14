using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;
    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;
    public GameObject rangeGfx;
    public int cost;
    public int sellPrice;
    public int upgradeCost;
    public int upgradedSellPrice;
    [HideInInspector]
    public int currentSellPrice;
}
