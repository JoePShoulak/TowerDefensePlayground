using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public GameObject gxf;

    private RectTransform rect;
    private Image image;
    private float healthPercent;

    void Start()
    {
        Hide();
        rect = gxf.GetComponent<RectTransform>();
        image = gxf.GetComponent<Image>();
    }

    void UpdateSize()
    {
        Vector3 newScale = rect.localScale;
        newScale.x = healthPercent;
        rect.localScale = newScale;
    }

    void UpdateColor()
    {
        image.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }

    public void Display(float _healthPercent)
    {
        healthPercent = _healthPercent;

        if (!gameObject.activeSelf) Show();
        UpdateSize();
        UpdateColor();
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
