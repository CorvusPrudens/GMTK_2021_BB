using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSoul : MonoBehaviour
{
    [HideInInspector]
    public int index = 0;
    public GameObject player;
    Vector2 offset;

    
    void Start()
    {
        offset = new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Util.Map(index, 0, 16, 1, 0);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);

        Vector2 currentPos = (Vector2) transform.position;
        Vector2 targ = player.GetComponent<PlayerMovement>().GetTrail() + offset;
        Vector2 move = (targ - currentPos) * Time.deltaTime * 2;
        transform.position = new Vector3(currentPos.x + move.x, currentPos.y + move.y, 0);
    }
}
