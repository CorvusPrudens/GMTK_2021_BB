using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspAggroTrigger : MonoBehaviour
{
    private WaspMovement movement;

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponentInParent<WaspMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement.speed = movement.baseSpeed * movement.aggroSpeedMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement.speed = movement.baseSpeed;
        }
    }
}
