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

    public GameObject DragonSoul;
    public GameObject RolySoul;
    public GameObject WaspSoul;

    //Movement vectors
    private Vector2 moveVector;
    private Vector2 normalVector;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxStats.health;
        changePlayerStatus = GetComponent<ChangePlayerStats>();
    }

    public void Kill()
    {
        // expect there to be exactly one player
        GameObject p = GameObject.FindGameObjectsWithTag("Player")[0];
        GameObject soul = new GameObject();
        // NOTE -- don't forget to uncomment
        if (enemyType == EnemyType.Wasp)
        {
            AkSoundEngine.PostEvent("Wasp_Die", this.gameObject);
            soul = Instantiate(WaspSoul, transform.position, new Quaternion(), p.transform.parent);
            
        }
        else if (enemyType == EnemyType.Dragonfly)
        {
            //AkSoundEngine.PostEvent("Dragon_Die", this.gameObject);
            soul = Instantiate(DragonSoul, transform.position, new Quaternion(), p.transform.parent);
        }
        else if (enemyType == EnemyType.RolyPoly)
        {
            AkSoundEngine.PostEvent("Rolly_Die", this.gameObject);
            soul = Instantiate(RolySoul, transform.position, new Quaternion(), p.transform.parent);
        }

        soul.GetComponent<BugSoul>().player = p;

        changePlayerStatus.ApplyStatsToPlayer();
        Destroy(gameObject);

    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;

        AkSoundEngine.PostEvent("Enemy_TakeDamage", this.gameObject);

        print("DAMAGE TO " + gameObject.name + ": " + damageTaken);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

}
