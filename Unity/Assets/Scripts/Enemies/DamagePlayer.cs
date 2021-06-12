using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private EnemyStats enemyStats;

    // Start is called before the first frame update
    void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
        playerStats.TakeDamage(enemyStats.maxStats.strength);
    }
}
