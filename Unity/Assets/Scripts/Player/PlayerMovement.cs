using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Basic Movement
    [SerializeField] private float moveSpeed;
    private float inputVertical;
    private float inputHorizontal;
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool canMove;

    private void Awake()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    private void Movement()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        movementVector = new Vector2(inputHorizontal, inputVertical);

        transform.Translate(movementVector * moveSpeed * Time.deltaTime);
    }
}
