using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    private enum FadeMode
    {
        None,
        FadeIn,
        FadeOut
    }

    public bool fadeAtStart;
    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;

    private FadeMode currentMode = FadeMode.None;
    private float currentFadeTime;
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = true;

        if (fadeAtStart)
            FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode == FadeMode.None)
            return;

        float fadeTime = currentFadeTime;
        Color color = new Color(0, 0, 0);

        if (currentMode == FadeMode.FadeOut)
        {
            color.a = fadeOutCurve.Evaluate(fadeTime);
        }
        else if (currentMode == FadeMode.FadeIn)
        {
            color.a = fadeInCurve.Evaluate(fadeTime);
        }

        img.color = color;

        currentFadeTime += Time.deltaTime;
    }

    /// <summary>
    /// Fade out the image. (Black -> transparent)
    /// </summary>
    public void FadeOut()
    {
        currentMode = FadeMode.FadeOut;
        currentFadeTime = 0;
    }

    /// <summary>
    /// Fade in the image. (Transparent -> Black)
    /// </summary>
    public void FadeIn()
    {
        currentMode = FadeMode.FadeIn;
        currentFadeTime = 0;
    }
}
