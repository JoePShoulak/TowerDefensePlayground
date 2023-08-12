using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public GameObject gxf;

    private Image image;
    private float healthPercent;

    void Start()
    {
        Hide();
        image = gxf.GetComponent<Image>();
    }

    public void Display(float _healthPercent)
    {
        healthPercent = _healthPercent;

        if (!gameObject.activeSelf) Show();
        image.fillAmount = healthPercent;
        image.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
