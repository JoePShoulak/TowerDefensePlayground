using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;
    public int cost;
    public GameObject buildEffect;
    public int upgradeCost;
    public int sellPrice;
    public int upgradedSellPrice;
    [HideInInspector]
    public int currentSellPrice;
}
