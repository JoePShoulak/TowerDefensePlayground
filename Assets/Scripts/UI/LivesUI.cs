using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesText;

    void Update()
    {
        livesText.text = Player.Lives + " LIVES";
    }
}
