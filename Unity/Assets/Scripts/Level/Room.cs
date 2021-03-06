using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject Doors;
    public GameObject Enemies;
    public GameObject Traps;
    public GameObject Chest;
    public GameObject Spawn;
    public GameObject Transition;

    [System.NonSerialized]
    public RoomPosition position;

    [System.NonSerialized]
    public bool visited = false;
    [System.NonSerialized]
    public bool key_item = false;
    [System.NonSerialized]
    public bool dead_end = false;
    [System.NonSerialized]
    public int distance = 0;

    [System.NonSerialized]
    public List<Door.Position> doorList = new List<Door.Position>();

    // This will absolutely have to be changed
    [System.NonSerialized]
    public List<Door.Position> lockedDoors = new List<Door.Position>();

    [System.NonSerialized]
    public string type = "normal";

    public GameObject player;

    public bool visible = false;
    bool prevvis = false;
    public float transitionSpeed = 4f;
    float transitionTick = 0;
    bool transin = false;
    bool transout = false;

    public bool active = false;
    bool inactive = true;

    public GameObject cam;

    public void EnableChest(bool state, bool key=false)
    {
        Chest.transform.GetChild(0).gameObject.SetActive(state);
        if (state)
        {
            if (key)
            {
                // idk do some key stuff here
            }
            else
            {
                // idk put some health in it or some shite
            }
        }
    }

    public List<Door.Position> GetDoors()
    {
        List<Door.Position> dirs = new List<Door.Position>();
        foreach (Transform child in Doors.transform)
        {
            Door door = child.gameObject.GetComponent<Door>();
            dirs.Add(door.position);
        }
        return dirs;
    }

    public void FadeIn(float speed=-1)
    {
        transin = true;
        if (speed > 0)
        {
            transitionSpeed = speed;
        }
    }

    public void FadeOut(float speed=-1)
    {
        transout = true;
        if (speed > 0)
        {
            transitionSpeed = speed;
        }
    }

    void Awake()
    {
        // for (int i = 0; i < transform.childCount - 2; i++)
        // {
        //     transform.GetChild(i).gameObject.SetActive(false);
        // }
        if (Doors != null)
        {
            Doors.SetActive(false);
            Traps.SetActive(false);
            Enemies.SetActive(false);
            Chest.SetActive(false);
        }
        
    }

    void OnValidate()
    {
        if (prevvis != visible)
        {
            if (visible) FadeIn();
            else FadeOut();
            prevvis = visible;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transin)
        {
            if (transitionTick <= 0)
            {
                // for (int i = 0; i < 4; i++)
                // {
                //     transform.GetChild(i).gameObject.SetActive(true);
                // }
                Doors.SetActive(true);
                Traps.SetActive(true);
                Enemies.SetActive(true);
                Chest.SetActive(true);
                active = true;
                // Spawn.SetActive(true);
            }
            if (transitionTick < 1.0f) transitionTick += transitionSpeed * Time.deltaTime;
            else
            {
                transin = false;
            }
            SpriteRenderer rend = Transition.GetComponent<SpriteRenderer>();
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1 - transitionTick);
        }
        else if (transout)
        {
            if (transitionTick > 0.0f) transitionTick -= transitionSpeed * Time.deltaTime;
            else
            {
                transout = false;
                // for (int i = 0; i < 4; i++)
                // {
                //     transform.GetChild(i).gameObject.SetActive(false);
                // }
                Doors.SetActive(false);
                Traps.SetActive(false);
                Enemies.SetActive(false);
                Chest.SetActive(false);
                active = false;
            }
            SpriteRenderer rend =  Transition.GetComponent<SpriteRenderer>();
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1 - transitionTick);
        }
        // if (player != null)
        // {
        //     Vector3 difference = player.transform.position - transform.position;
        //     if (difference.x < 7.5 && difference.x > -7.5 && difference.y < 7.5 && difference.y > -7.5)
        //     {
        //         if (inactive)
        //         {
        //             inactive = false;
        //             FadeIn();
        //             active = true;
        //             Vector3 newpos = transform.position;
        //             newpos.z = -10;
        //             cam.transform.position = newpos;
        //         }
        //     }
        //     else if (difference.x > 8 || difference.x < -8 || difference.y > 8 || difference.y < -8)
        //     {
        //         if (active)
        //         {
        //             active = false;
        //             FadeOut();
        //             inactive = true;
        //         }
        //     }
        // }
        

    }
}
