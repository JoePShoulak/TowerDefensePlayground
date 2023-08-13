using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded;
    public GameObject gameOverUI;
    public GameObject winScreenUI;

    public string nextLevel = "Level 02";
    public int levelToUnlock = 2;

    public static string NextLevel;

    void Start()
    {
        GameEnded = false;
        NextLevel = nextLevel;
    }

    void Update()
    {
        if (GameEnded) return;

        if (Player.Lives <= 0) Lose();
    }

    public void Win()
    {
        Debug.Log("you won!");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        EndGame(winScreenUI);
    }

    public void Lose()
    {
        Debug.Log("you lost!");
        EndGame(gameOverUI);
    }

    public void EndGame(GameObject uiToShow)
    {
        GameEnded = true;
        uiToShow.SetActive(true);
    }
}
