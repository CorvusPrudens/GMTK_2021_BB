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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
