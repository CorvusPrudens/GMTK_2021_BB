using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangePlayerStats))]
public class EnemyStats : MonoBehaviour, IDamageable
{
    public Stats maxStats;
    private float currentHealth;

    private ChangePlayerStats changePlayerStatus;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxStats.health;
        changePlayerStatus = GetComponent<ChangePlayerStats>();
    }

    public void Kill()
    {
        changePlayerStatus.ApplyStatsToPlayer();
        Destroy(gameObject);
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

}
