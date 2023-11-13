using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private GameObject collectParticle;
    [SerializeField]
    private GameObject despawnParticle;

    private void Start()
    {
        StartCoroutine(LifeCycle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MinigameManager.instance.HealPlayer();
            Instantiate(collectParticle, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private IEnumerator LifeCycle()
    {
        Animator anim = GetComponent<Animator>();

        yield return new WaitForSeconds(lifeTime);

        anim.SetTrigger("Despawn");
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1);

        Instantiate(despawnParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
