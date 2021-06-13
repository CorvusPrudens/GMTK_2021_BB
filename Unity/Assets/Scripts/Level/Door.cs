using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    
    public bool isLockable = false;
    public bool isLocked = false;
    public float HP = 10;

    public Sprite lockedSprite;
    public Sprite brokenSprite;
    public Sprite openSprite;

    private bool inRange;
    private PlayerStats stats;
    [SerializeField] private GameObject lockedCollision;

    public enum Position {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_L,
        DOWN_L,
        LEFT_L,
        RIGHT_L,
    }

    public static Position Opposite(Position pos) {
        switch (pos)
        {
            default:
            case Position.UP:
                return Position.DOWN;
            case Position.DOWN:
                return Position.UP;
            case Position.LEFT:
                return Position.RIGHT;
            case Position.RIGHT:
                return Position.LEFT;

            case Position.UP_L:
                return Position.DOWN_L;
            case Position.DOWN_L:
                return Position.UP_L;
            case Position.LEFT_L:
                return Position.RIGHT_L;
            case Position.RIGHT_L:
                return Position.LEFT_L;
        }
    }

    public static Position Lock(Position pos) {
        switch (pos)
        {
            default:
            case Position.UP:
                return Position.DOWN_L;
            case Position.DOWN:
                return Position.UP_L;
            case Position.LEFT:
                return Position.RIGHT_L;
            case Position.RIGHT:
                return Position.LEFT_L;
        }
    }

    public Position position = Position.UP;

    void SetVisuals()
    {
        SpriteRenderer rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        switch (position)
        {
            case Position.UP:
                rend.sprite = openSprite;
                break;
            case Position.DOWN:
                rend.sprite = openSprite;
                transform.eulerAngles = new Vector3(0, 0, 180);
                lockedCollision.SetActive(false);
                break;
            case Position.LEFT:
                rend.sprite = openSprite;
                transform.eulerAngles = new Vector3(0, 0, 90);
                lockedCollision.SetActive(false);
                break;
            case Position.RIGHT:
                rend.sprite = openSprite;
                transform.eulerAngles = new Vector3(0, 0, -90);
                lockedCollision.SetActive(false);
                break;

            case Position.UP_L:
                rend.sprite = lockedSprite;
                lockedCollision.SetActive(true);
                break;
            case Position.DOWN_L:
                rend.sprite = lockedSprite;
                transform.eulerAngles = new Vector3(0, 0, 180);
                lockedCollision.SetActive(true);
                break;
            case Position.LEFT_L:
                rend.sprite = lockedSprite;
                transform.eulerAngles = new Vector3(0, 0, 90);
                lockedCollision.SetActive(true);
                break;
            case Position.RIGHT_L:
                rend.sprite = lockedSprite;
                transform.eulerAngles = new Vector3(0, 0, -90);
                lockedCollision.SetActive(true);
                break;
        }
    }

    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        SetVisuals();
    }

    void OnEnable()
    {
        SetVisuals();
    }

    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && stats.keys > 0)
            {
                UnlockDoor();
            }
        }
    }

    void UnlockDoor()
    {
        stats.keys--;
        EventBroker.CallUpdateStatsUI();

        AkSoundEngine.PostEvent("Player_UnlockDoor", this.gameObject);

        switch (position)
        {
            case Position.UP_L:
                position = Position.UP;
                break;
            case Position.DOWN_L:
                position = Position.DOWN;
                break;
            case Position.LEFT_L:
                position = Position.LEFT;
                break;
            case Position.RIGHT_L:
                position = Position.RIGHT;
                    break;
        }
        
        SetVisuals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}