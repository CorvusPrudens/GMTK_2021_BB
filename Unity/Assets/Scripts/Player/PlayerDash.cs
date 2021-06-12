using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //References
    private PlayerStats stats;
    private BoxCollider2D playerCollider;
    private PlayerMovement movement;
    //Dash parameters
    private float startDashTime = 0.2f;
    private float dashTime;
    private bool isDashing;
    private Vector2 dashDirection;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        playerCollider = GetComponent<BoxCollider2D>();
        movement = GetComponent<PlayerMovement>();

        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartDash();
        }
        if (isDashing)  
        {
            Dash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        movement.canMove = false;
        dashDirection = movement.movementVector;
    }

    void ResetDash()
    {
        dashTime = startDashTime;
        isDashing = false;
        movement.canMove = true;
        playerCollider.enabled = true;
    }

    void Dash()
    {
        if(dashTime <= 0)
        {
            ResetDash();
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerCollider.enabled = false;
            transform.Translate(dashDirection * stats.maxStats.speed * Time.deltaTime);
        }
    }
}
