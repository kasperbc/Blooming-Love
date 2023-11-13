using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameParticle : MonoBehaviour
{
    ParticleSystem parSystem;
    void Start()
    {
        parSystem = GetComponent<ParticleSystem>();
        Invoke(nameof(DestroySelf), parSystem.main.duration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
