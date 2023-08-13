using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Fix the fact that scrolling doesn't work unless you're over a button
public class LevelSelector : MonoBehaviour
{
    public Transition transition;
    public List<Button> levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = levelReached; i < levelButtons.Count; i++)
        {
            Button button = levelButtons[i];
            button.interactable = false;
        }
    }

    public void Select(string levelName)
    {
        transition.FadeTo(levelName);
    }
}
