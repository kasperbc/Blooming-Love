using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move() { }

    protected virtual void OnHitPlayer()
    {
        MinigameManager.instance.DamagePlayer();

        Destroy(gameObject);
    }

    protected virtual void OnHitBoundary()
    {
        print("Hit boundary!");

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnHitPlayer();
        }
        else if (collision.CompareTag("MinigameBoundary"))
        {
            OnHitBoundary();
        }
    }
}
