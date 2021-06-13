using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    [SerializeField] private float damage;
    PlayerMovement playerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerMove = collision.gameObject.GetComponent<PlayerMovement>();
            playerStats.TakeDamage(damage);
            StartCoroutine(ResetPlayer());
        }
    }

    private IEnumerator ResetPlayer()
    {
        playerMove.canMove = false;
        playerMove.rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.5f);
        playerMove.canMove = true;
        playerMove.ResetToSpawn();
    }
}
