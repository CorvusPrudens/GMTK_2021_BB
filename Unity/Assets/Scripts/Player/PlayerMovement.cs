using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References
    private PlayerStats stats;
    private BoxCollider2D playerCollider;

    //Basic Movement
    [SerializeField] private float moveSpeed;
    private float inputVertical;
    private float inputHorizontal;
    [HideInInspector] public Vector2 movementVector;

    //Dash
    private float startDashTime = 0.3f;
    private float dashTime;
    private bool isDashing;
    private Vector2 dashDirection;

    void Update()
    {
        if (!isDashing)
        {
            Movement();
        }
        else if (isDashing)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartDash();
        }
    }

    private void Movement()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        movementVector = new Vector2(inputHorizontal, inputVertical);

        transform.Translate(movementVector * moveSpeed * Time.deltaTime);
    }

    void StartDash()
    {
        isDashing = true;
        dashDirection = movementVector;
    }

    void Dash()
    {
        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            isDashing = false;
            playerCollider.enabled = true;
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerCollider.enabled = false;
            transform.Translate(dashDirection * stats.maxStats.speed * Time.deltaTime);
        }
    }

}
