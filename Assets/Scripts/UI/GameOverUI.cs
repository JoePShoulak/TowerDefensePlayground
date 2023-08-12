using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text roundsText;
    public Transition transition;

    void OnEnable()
    {
        roundsText.text = Player.RoundsSurvived.ToString();
    }

    public void Retry()
    {
        transition.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        transition.FadeTo("MainMenu");
    }
}
