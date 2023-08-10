using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text roundsText;

    void OnEnable()
    {
        roundsText.text = Player.RoundsSurvived.ToString();
    }
}