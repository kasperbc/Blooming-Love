using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyProjectileShooter : ScriptedProjectileShooter
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;

        base.Start();
    }

    protected override void ShootProjectile()
    {
        print("Shooting");
        anim.SetTrigger("Grow");
        spriteRenderer.enabled = true;
        Invoke(nameof(OnShootProjectile), 1.5f);
    }

    private void OnShootProjectile()
    {
        anim.SetTrigger("Degrow");
        base.ShootProjectile();
        Invoke(nameof(base.DestroySelf), 1);
    }
}
