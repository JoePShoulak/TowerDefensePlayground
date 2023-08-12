using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public Transition transition;

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
        transition.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        TimeManager.Resume();
        transition.FadeTo("MainMenu");
    }
}
