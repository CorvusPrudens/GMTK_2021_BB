using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyPollyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private Rigidbody2D rb;
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = stats.maxStats.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Pit") || collision.gameObject.CompareTag("Spike"))
        {
            
        }
    }
}
