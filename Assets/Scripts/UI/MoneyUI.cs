using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;


    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + Player.Money.ToString();
    }
}
