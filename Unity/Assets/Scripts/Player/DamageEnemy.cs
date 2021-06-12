using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemy = collision.GetComponent<EnemyStats>();
        enemy.TakeDamage(playerStats.maxStats.strength);
    }
}
