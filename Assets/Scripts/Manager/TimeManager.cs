using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public PauseUI pauseUI;

    public static void Toggle()
    {
        Time.timeScale = 1f - Time.timeScale;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (GetInput.Pause())
        {
            Toggle();
            pauseUI.Toggle();
        }
    }


}
