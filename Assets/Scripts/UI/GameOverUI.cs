using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Transition transition;

    public void Retry()
    {
        transition.FadeToSelf();
    }

    public void Menu()
    {
        transition.FadeToMenu();
    }
}
