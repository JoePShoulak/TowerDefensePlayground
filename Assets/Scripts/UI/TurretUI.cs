using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretUI : MonoBehaviour
{
    private Node target;
    public GameObject upgradeButton;
    public TMP_Text upgradeText;
    public TMP_Text sellText;

    public void Show(Node node)
    {
        target = node;

        if (node.isUpgraded) upgradeButton.SetActive(false);
        else upgradeButton.SetActive(true);

        transform.position = target.BuildPosition;
        // TOOO: Make these separate text objects for easier scripting
        upgradeText.text = "<b>Upgrade</b>\n$" + node.blueprint.upgradeCost;
        sellText.text = "<b>Sell</b>\n$" + node.blueprint.currentSellPrice;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
