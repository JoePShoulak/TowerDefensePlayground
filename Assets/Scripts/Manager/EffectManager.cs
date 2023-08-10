using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static void Spawn(float killTime, GameObject _effect, Vector3 position, Quaternion rotation)
    {
        GameObject effect = (GameObject)Instantiate(_effect, position, rotation);
        Destroy(effect, killTime);
    }
    public static void Spawn(float killTime, GameObject _effect, Vector3 position)
    {
        Spawn(killTime, _effect, position, Quaternion.identity);
    }
}
