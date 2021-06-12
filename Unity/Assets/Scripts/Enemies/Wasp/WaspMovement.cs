using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspMovement : MonoBehaviour
{
    private GameObject player;
    private EnemyStats stats;
    public float aggroSpeedMultiplier;
    [HideInInspector] public float baseSpeed;
    [HideInInspector] public float speed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        baseSpeed = stats.maxStats.speed;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }   
}
