using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyPollyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private Rigidbody2D rb;
    private float speed;
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = stats.maxStats.speed;
        moveVector = new Vector2(1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = moveVector * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Pit") || collision.gameObject.CompareTag("Spike"))
        {
            float speed = rb.velocity.magnitude;
            Vector2 inNormal = collision.contacts[0].normal;
            Vector2 direction = Vector2.Reflect(rb.velocity.normalized, inNormal.normalized);
            moveVector = direction;
        }
    }
}