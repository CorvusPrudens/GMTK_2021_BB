using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Stats Reference
    private PlayerStats stats;
    //Movement
    private float inputVertical;
    private float inputHorizontal;
    private Vector2 movementVector;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        movementVector = new Vector2(inputHorizontal, inputVertical);

        transform.Translate(movementVector * stats.maxStats.speed * Time.deltaTime);
    }
}
