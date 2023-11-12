using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvFrameColor : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField, Range(0,1)]
    private float intensity = 0.5f;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float timeSinceDamageNormalized = 
            Mathf.Clamp01(MinigameManager.instance.timeSinceLastDamage / MinigameManager.instance.playerIFrames);

        float gb = timeSinceDamageNormalized;

        Color c = new Color(1, gb, gb);
        sr.color = c;
    }
}
