using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int RoundsSurvived;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        RoundsSurvived = 0;
    }

    public static void TakeDamage(int damage)
    {
        Lives = (int)Mathf.Max(0f, Lives - damage);
    }
}
