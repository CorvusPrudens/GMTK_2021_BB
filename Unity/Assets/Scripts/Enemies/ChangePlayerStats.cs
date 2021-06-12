using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerStats : MonoBehaviour
{
    public Stats statsToApply;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        playerStats.UpdateMaxStats(statsToApply);
        print("COLLISION");
    }
}
