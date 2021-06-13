using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //References
    private PlayerStats stats;
    private BoxCollider2D playerCollider;
    private PlayerMovement movement;
    private Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();

        dashTime = startDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        AkSoundEngine.PostEvent("Player_Dash", this.gameObject);

        print("IS DASHING");
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
            rb.velocity = dashDirection * stats.maxStats.speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Pit"))
            {
                Physics2D.IgnoreLayerCollision(8, 6);
            }
        }
    }
}
