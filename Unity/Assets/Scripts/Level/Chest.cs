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

    [SerializeField] private Sprite key;
    [SerializeField] private Sprite heart;
    [SerializeField] private SpriteRenderer collectibleRenderer;

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
            AkSoundEngine.PostEvent("Player_CollectSoul", this.gameObject);
            stats.maxStats.health += healthAwarded;
            stats.currentHealth += healthAwarded;
            DisplaySprite(heart);
        }
        else if(chestType == ChestType.Key)
        {
            AkSoundEngine.PostEvent("Player_CollectKey", this.gameObject);
            stats.keys++;
            DisplaySprite(key);
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

    private void DisplaySprite(Sprite sprite)
    {
        collectibleRenderer.sprite = sprite;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        float currentTime = 0f;
        float fadeDuration = 2f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, currentTime / fadeDuration);
            collectibleRenderer.color =
                new Color(collectibleRenderer.color.r, collectibleRenderer.color.g, collectibleRenderer.color.b, newAlpha);
            yield return null;
        }
        yield break;
    }

}
