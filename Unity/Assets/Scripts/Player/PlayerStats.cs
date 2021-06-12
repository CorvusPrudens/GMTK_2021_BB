using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IUpdateStats
{
    public Stats maxStats;
    [HideInInspector] public float currentHealth;

    private void Awake()
    {
        EventBroker.applyPlayerStats += UpdateMaxStats;
        currentHealth = maxStats.health;
    }

    public void UpdateMaxStats(Stats statsToApply)
    {
        maxStats.speed += statsToApply.speed;
        maxStats.strength += statsToApply.strength;
        maxStats.health += statsToApply.health;

        currentHealth += statsToApply.health;
        print("health: " + maxStats.health + " strength: " + maxStats.strength + " speed: " + maxStats.speed);

        EventBroker.CallUpdateStatsUI();
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        print("DAMAGE TO PLAYER: " + damageTaken);

        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public  void Kill()
    {
        Destroy(gameObject);
    }
}
