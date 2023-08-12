using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Image image;
    public float duration = 1f;
    public AnimationCurve curve;

    public Color color;

    void Start()
    {
        FadeIn();
    }

    Color GenerateColor(float a)
    {
        return new Color(color.r, color.g, color.b, a);
    }

    IEnumerator PerformFade(bool rev = false, string scene = null)
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / duration;
            image.color = GenerateColor(curve.Evaluate(rev ? 1f - t : t));
            yield return 0;
        }

        if (scene != null) SceneManager.LoadScene(scene);
    }

    public void FadeIn()
    {
        StartCoroutine(PerformFade());
    }

    public void FadeOut()
    {
        StartCoroutine(PerformFade(true));
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(PerformFade(true, scene));
    }

    // IEnumerator FadeIn()
    // {
    //     float t = 1f;

    //     while (t > 0)
    //     {
    //         t -= Time.deltaTime / duration;
    //         image.color = GenerateColor(curve.Evaluate(t));
    //         yield return 0;
    //     }

    // }

    // IEnumerator FadeOut(string scene)
    // {
    //     float t = 0f;

    //     while (t < 1)
    //     {
    //         t += Time.deltaTime / duration;
    //         image.color = GenerateColor(curve.Evaluate(t));
    //         yield return 0;
    //     }

    //     SceneManager.LoadScene(scene);
    // }


}
