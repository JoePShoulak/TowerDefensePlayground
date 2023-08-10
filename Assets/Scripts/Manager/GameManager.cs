using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded;
    public GameObject gameOverUI;

    void Start()
    {
        GameEnded = false;
    }

    void Update()
    {
        if (GameEnded) return;

        if (Player.Lives <= 0)
        {
            GameEnded = true;
            gameOverUI.SetActive(true);
        }

    }
}
