using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
        }
    }
}
