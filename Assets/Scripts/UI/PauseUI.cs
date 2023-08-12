using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Continue()
    {
        Hide();
        TimeManager.Resume();
    }

    public void Retry()
    {
        TimeManager.Resume();
        SceneController.Restart();
    }

    public void Menu()
    {
        TimeManager.Resume();
        SceneController.LoadMainMenu();
    }
}
