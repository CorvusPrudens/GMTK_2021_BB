using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspMovement : MonoBehaviour
{
    private GameObject player;
    private EnemyStats stats;
    private Vector3 startPos;
    [HideInInspector] public bool aggro;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (aggro)
        {
            Move(player.transform.position);
        }
        else
        {
            Move(startPos);
        }
    }

    void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, stats.maxStats.speed * Time.deltaTime);
        //transform.Translate((target - transform.position).normalized * stats.maxStats.speed * Time.deltaTime);
    }   
}
