using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable, IUpdateStats
{
    public Stats maxStats;
    [HideInInspector] public float currentHealth;

    public float speedAdjustment = 41.26f;
    float adjustedSpeed;

    [SerializeField]
    private AK.Wwise.State speedState;
    [SerializeField]
    private AK.Wwise.State strengthState;
    [SerializeField]
    private AK.Wwise.State healthState;

    private void Awake()
    {
        EventBroker.applyPlayerStats += UpdateMaxStats;
        currentHealth = maxStats.health;

        adjustedSpeed = maxStats.speed - speedAdjustment;
    }

    public void UpdateMaxStats(Stats statsToApply)
    {
        maxStats.speed += statsToApply.speed;
        maxStats.strength += statsToApply.strength;
        maxStats.health += statsToApply.health;

        if (adjustedSpeed > maxStats.strength && adjustedSpeed > maxStats.health)
        {
            speedState.SetValue();
        }

        if (maxStats.health > maxStats.strength && maxStats.health > adjustedSpeed)
        {
            healthState.SetValue();
        }

        if (maxStats.strength > adjustedSpeed && maxStats.strength > maxStats.health)
        {
            strengthState.SetValue();
        }

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
