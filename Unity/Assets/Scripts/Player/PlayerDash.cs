using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //References
    private Rigidbody2D playerRb;
    private PlayerStats stats;
    private BoxCollider2D playerCollider;
    private PlayerMovement movement;
    //Dash parameters
    private float dashSpeed = 1;
    private float startDashTime = 0.3f;
    private float dashTime;

    private bool canDash = true;
    private bool isDashing;

    private Vector2 dashDirection;

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
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
            isDashing = true;
            dashDirection = movement.movementVector;
            print("IS DASHING");
        }
        if (isDashing)
        {
            Dash();
        }
    }

    void Dash()
    {
        if(dashTime <= 0)
        {
            dashTime = startDashTime;
            isDashing = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerCollider.enabled = false;
            transform.Translate(dashDirection * stats.maxStats.speed * Time.deltaTime);
        }
    }
}
