using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public enum ChestType { Key, HP };
    [SerializeField] private ChestType chestType;
    private bool canOpen = false;
    private bool isOpen = false;
    private float healthAwarded = 4;

    [SerializeField] private Sprite openSprite;
    private SpriteRenderer renderer;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Open();
            }
        }
    }

    private void Open()
    {
        renderer.sprite = openSprite;
        isOpen = true;
        AkSoundEngine.PostEvent("Player_OpenChest", this.gameObject);

        if (chestType == ChestType.HP)
        {
            stats.maxStats.health += healthAwarded;
            stats.currentHealth += healthAwarded;
        }
        else if(chestType == ChestType.Key)
        {
            stats.keys++;
        }

        EventBroker.CallUpdateStatsUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canOpen = false;
        }
    }

}
