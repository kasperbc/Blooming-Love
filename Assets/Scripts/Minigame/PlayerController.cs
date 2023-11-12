using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float turnDir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveAxis = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveAxis.x > 0)
        {
            sprite.transform.localScale = Vector3.one;
        }
        else if (moveAxis.x < 0)
        {
            sprite.transform.localScale = new(-1, 1, 1);
        }

        sprite.GetComponent<Animator>().SetFloat("MoveAmount", (moveAxis.x + 1) / 2); 

        rb.velocity = moveAxis * moveSpeed;
    }
}
