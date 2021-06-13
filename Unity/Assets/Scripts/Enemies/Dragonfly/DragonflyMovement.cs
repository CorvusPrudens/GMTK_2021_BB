using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    [SerializeField] [Range(1f, 10f)] private float minWaitTime, maxWaitTime;
    private Vector2 screenBounds;
    private Vector2 targetPosition;
    private float offsetX = 0, offsetY = 0;
    private EnemyStats stats;
    private bool canMove;
    private bool toggle = true;
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
        Vector3 v = transform.position;
        if (canMove)
        {
            Move();
        }
        float distance = Vector3.Distance(v, transform.position);
        if (distance > 0.005f)
        {
            if (toggle)
            {
                AkSoundEngine.PostEvent("Dragon_Idle", this.gameObject);
                toggle = false;
            }
        }
        else
        {
            if (!toggle)
            {
                AkSoundEngine.PostEvent("STOPALL_local", this.gameObject);
                toggle = true;
            }
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
        float randomPosX = Random.Range(-screenBounds.x + offsetX, screenBounds.x - offsetX);
        float randomPosY = Random.Range(-screenBounds.y + offsetY, screenBounds.y - offsetY);

        targetPosition = new Vector2(randomPosX, randomPosY);   
    }
}
