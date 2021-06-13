using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] variations;
    void Start()
    {
        SpriteRenderer rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rend.sprite = variations[(int) Random.Range(0, variations.Length)];
    }
}
