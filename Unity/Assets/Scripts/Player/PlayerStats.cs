using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IUpdateStats
{
    public Stats maxStats;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxStats.health;
    }

    public void UpdateMaxStats(Stats statsToApply)
    {
        maxStats.speed += statsToApply.speed;
        maxStats.strength += statsToApply.strength;
        maxStats.health += statsToApply.health;

        currentHealth += statsToApply.health;
        print(statsToApply);
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth += damageTaken;

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
