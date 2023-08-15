using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    public static TurretManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one TurretManager in scene");
            return;
        }
        instance = this;
    }

    public void OnValidate()
    {
        // TODO: Probably more validation
        standardTurret.cost = Mathf.RoundToInt(Mathf.Max(0f, (float)standardTurret.cost));
        missileLauncher.cost = Mathf.RoundToInt(Mathf.Max(0f, (float)missileLauncher.cost));
        laserBeamer.cost = Mathf.RoundToInt(Mathf.Max(0f, (float)laserBeamer.cost));
    }

}
