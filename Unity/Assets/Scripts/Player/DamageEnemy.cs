using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public GameObject player;
    private PlayerStats playerStats;
    private float multiplier = 0.25f;

    private void Awake()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            enemy.TakeDamage(playerStats.maxStats.strength * multiplier);
        }
    }
}
