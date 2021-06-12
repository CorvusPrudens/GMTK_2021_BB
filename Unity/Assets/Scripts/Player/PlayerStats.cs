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
        EventBroker.CallUpdateStatsUI();

        print("health: " + maxStats.health + " strength: " + maxStats.strength + " speed: " + maxStats.speed);
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        EventBroker.CallUpdateStatsUI();

        AkSoundEngine.PostEvent("Player_TakeDamage", this.gameObject);

        if(currentHealth <= 0)
        {
            Kill();
        }

        print("DAMAGE TO PLAYER: " + damageTaken);

    }

    public  void Kill()
    {
        AkSoundEngine.PostEvent("Player_Die", this.gameObject);

        Destroy(gameObject);
    }
}
