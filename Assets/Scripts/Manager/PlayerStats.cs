using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    public static void TakeDamage(int damage)
    {
        Lives = (int)Mathf.Max(0f, Lives - damage);
    }
}
