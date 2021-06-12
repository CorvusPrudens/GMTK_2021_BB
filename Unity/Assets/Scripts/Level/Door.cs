using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    
    public bool isLockable = false;
    public bool isLocked = false;
    public float HP = 10;

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

    void Start()
    {

    }

    void Update()
    {

    }


}