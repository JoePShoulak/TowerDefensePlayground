using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadSample()
    {
        SceneManager.LoadScene("SampleLevel");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
