using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded = false;


    // Update is called once per frame
    void Update()
    {
        if (GameEnded) return;

        if (Player.Lives <= 0)
        {
            GameEnded = true;
            Debug.Log("Game Over!");
        }

    }
}
