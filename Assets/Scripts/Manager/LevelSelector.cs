using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public Transition transition;

    public void Select(string levelName)
    {
        transition.FadeTo(levelName);
    }
}
