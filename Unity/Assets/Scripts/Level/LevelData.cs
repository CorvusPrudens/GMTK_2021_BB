using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomPosition 
{
    public int x;
    public int y;

    public RoomPosition(int xpos, int ypos) {
        x = xpos;
        y = ypos;
    }

    public RoomPosition Up() {
        return new RoomPosition(x, y - 1);
    }
    public RoomPosition Down() {
        return new RoomPosition(x, y + 1);
    }
    public RoomPosition Left() {
        return new RoomPosition(x - 1, y);
    }
    public RoomPosition Right() {
        return new RoomPosition(x + 1, y);
    }

    public static bool operator ==(RoomPosition a, RoomPosition b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(RoomPosition a, RoomPosition b) => a.x != b.x || a.y != b.y;
}