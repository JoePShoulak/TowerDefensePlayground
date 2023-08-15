using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class WinScreenUI : MonoBehaviour
{
    public Transition transition;

    public void Next()
    {
        transition.FadeTo(GameManager.NextLevel);
        Debug.Log("Calling load of scene: " + GameManager.NextLevel);
    }

    public void Menu()
    {
        transition.FadeTo("MainMenu");
    }
}
