using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyPollyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private Rigidbody2D rb;
    private float speed;
    private Vector2 moveVector;
    private Vector2 lastVector;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = stats.maxStats.speed;
        float velocityX = Random.Range(-1f, 1f);
        float velocityY = Random.Range(-1f, 1f);
        moveVector = new Vector2(velocityX, velocityY).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = moveVector * speed;
        lastVector = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Pit") ||
            collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Player"))
        {
            Vector2 inNormal = collision.contacts[0].normal;
            Vector2 direction = Vector2.Reflect(lastVector, inNormal).normalized;
            moveVector = direction;
        }
    }
}