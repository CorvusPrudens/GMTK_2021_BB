using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    [SerializeField] [Range(1f, 10f)] private float minWaitTime, maxWaitTime;
    private Vector2 screenBounds;
    private Vector2 targetPosition;
    private EnemyStats stats;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStats>();
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(WaitToMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, stats.maxStats.speed * Time.deltaTime);
    }

    private IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        GenerateRandomPosition();
        canMove = true;
        StartCoroutine(WaitToMove());
    }

    private void GenerateRandomPosition()
    {
        float randomPosX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomPosY = Random.Range(-screenBounds.y, screenBounds.y);

        targetPosition = new Vector2(randomPosX, randomPosY);   
    }
}
