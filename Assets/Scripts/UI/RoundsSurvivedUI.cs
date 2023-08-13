using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundsSurvivedUI : MonoBehaviour
{
    private TMP_Text roundsText;
    public float tickScale = 1f;
    public float startDelay = 1f;

    void OnEnable()
    {
        roundsText = GetComponent<TMP_Text>();
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int round = 0;
        roundsText.text = "0";
        yield return new WaitForSeconds(startDelay);

        int total = Player.RoundsSurvived;
        while (round < total)
        {
            float slowEffect = (1.1f - ((float)round / (float)total));
            roundsText.text = (++round).ToString();
            yield return new WaitForSeconds(tickScale / Mathf.Sqrt(total) / slowEffect);
        }
    }
}
