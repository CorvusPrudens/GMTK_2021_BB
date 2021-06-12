using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangePlayerStats))]
public class EnemyStats : MonoBehaviour, IDamageable
{
    public Stats maxStats;
    private float currentHealth;
    private ChangePlayerStats changePlayerStatus;
    public EnemyType enemyType;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxStats.health;
        changePlayerStatus = GetComponent<ChangePlayerStats>();
    }

    public void Kill()
    {
        if (enemyType == EnemyType.Wasp)
        {
            AkSoundEngine.PostEvent("Wasp_Die", this.gameObject);
        }
        else if (enemyType == EnemyType.Dragonfly)
        {
            AkSoundEngine.PostEvent("Dragon_Die", this.gameObject);
        }
        else if (enemyType == EnemyType.RolyPoly)
        {
            AkSoundEngine.PostEvent("Rolly_Die", this.gameObject);
        }

        changePlayerStatus.ApplyStatsToPlayer();
        Destroy(gameObject);
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        if (enemyType == EnemyType.Wasp)
        {
            AkSoundEngine.PostEvent("Wasp_TakeDamage", this.gameObject);
        }
        else if (enemyType == EnemyType.Dragonfly)
        {
            AkSoundEngine.PostEvent("Dragon_TakeDamage", this.gameObject);
        }
        else if (enemyType == EnemyType.RolyPoly)
        {
            AkSoundEngine.PostEvent("Rolly_TakeDamage", this.gameObject);
        }

        print("DAMAGE TO " + gameObject.name + ": " + damageTaken);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

}
