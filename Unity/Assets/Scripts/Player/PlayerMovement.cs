using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Basic Movement
    [SerializeField] private float moveSpeed;
    private float inputVertical;
    private float inputHorizontal;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool canMove;

    public GameObject spriteLeft;
    public GameObject spriteRight;
    public GameObject spriteUp;
    public GameObject spriteDown;

    enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }
    Direction lastDirection = Direction.RIGHT;

    void ActivateDirection(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                spriteUp.SetActive(true);
                spriteDown.SetActive(false);
                spriteLeft.SetActive(false);
                spriteRight.SetActive(false);
                break;
            case Direction.DOWN:
                spriteUp.SetActive(false);
                spriteDown.SetActive(true);
                spriteLeft.SetActive(false);
                spriteRight.SetActive(false);
                break;
            case Direction.LEFT:
                spriteUp.SetActive(false);
                spriteDown.SetActive(false);
                spriteLeft.SetActive(true);
                spriteRight.SetActive(false);
                break;
            case Direction.RIGHT:
                spriteUp.SetActive(false);
                spriteDown.SetActive(false);
                spriteLeft.SetActive(false);
                spriteRight.SetActive(true);
                break;
        }
    }

    void PlayDirection(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                spriteUp.GetComponent<AnimationController>().Play();
                break;
            case Direction.DOWN:
                spriteDown.GetComponent<AnimationController>().Play();
                break;
            case Direction.LEFT:
                spriteLeft.GetComponent<AnimationController>().Play();
                break;
            case Direction.RIGHT:
                spriteRight.GetComponent<AnimationController>().Play();
                break;
        }
    }

    void StopDirection(Direction d)
    {
        switch (d)
        {
            case Direction.UP:
                spriteUp.GetComponent<AnimationController>().Stop();
                break;
            case Direction.DOWN:
                spriteDown.GetComponent<AnimationController>().Stop();
                break;
            case Direction.LEFT:
                spriteLeft.GetComponent<AnimationController>().Stop();
                break;
            case Direction.RIGHT:
                spriteRight.GetComponent<AnimationController>().Stop();
                break;
        }
    }

    Direction GetDirection(Vector2 v)
    {
        float angle = Mathf.Atan2(v.y, v.x);
        if (angle > -Mathf.PI * 0.25 && angle < Mathf.PI * 0.25) return Direction.RIGHT;
        else if (angle > Mathf.PI * 0.25 && angle < Mathf.PI * 0.75) return Direction.UP;
        else if (angle > Mathf.PI * 0.75 || angle < -Mathf.PI * 0.75) return Direction.LEFT;
        return Direction.DOWN;
    }

    void ManageSprites(Vector2 v)
    {
        float mag = v.magnitude;
        if (mag > 0.01)
        {
            Direction d = GetDirection(v);
            if (d != lastDirection)
            {
                lastDirection = d;
                ActivateDirection(d);
            }
            PlayDirection(lastDirection);
        }
        else
        {
            StopDirection(lastDirection);
        }
    }

    private void Awake()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
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

        ManageSprites(movementVector);

        //transform.Translate(movementVector * moveSpeed * Time.deltaTime);
        rb.velocity = (movementVector * moveSpeed);
    }
}
