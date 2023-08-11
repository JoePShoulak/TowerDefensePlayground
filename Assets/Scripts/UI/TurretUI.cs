using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurretUI : MonoBehaviour
{
    private Node target;
    public TMP_Text upgradeText;
    public TMP_Text sellText;

    public void Show(Node node)
    {
        target = node;
        transform.position = target.BuildPosition;
        upgradeText.text = "<b>Upgrade</b>\n$" + node.blueprint.upgradeCost;
        sellText.text = "<b>Sell</b>\n$" + node.blueprint.sellPrice;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
