using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    public AnimationCurve fadeCurve;
    public float fadeDuration = 1;

    private bool fading;
    private float currentFadeTime;

    private SpriteRenderer backgroundChild;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        backgroundChild = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            FadeBackground();
        }
    }

    private void FadeBackground()
    {
        Color c = new(1, 1, 1)
        {
            a = fadeCurve.Evaluate(currentFadeTime / fadeDuration)
        };

        spriteRenderer.color = c;

        if (currentFadeTime / fadeDuration >= 1)
        {
            OnFadeFinished();
        }

        currentFadeTime += Time.deltaTime;
    }

    public void FadeToBackground(Sprite background)
    {
        backgroundChild.sprite = background;
        fading = true;
        currentFadeTime = 0;
    }

    public void SetBackgroundInstant(Sprite background)
    {
        spriteRenderer.sprite = background;
    }

    private void OnFadeFinished()
    {
        fading = false;
        spriteRenderer.sprite = backgroundChild.sprite;
        spriteRenderer.color = Color.white;
    }
}
