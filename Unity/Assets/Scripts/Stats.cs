using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    public float health, speed, strength;

    public Stats(float health, float speed, float strength)
    {
        this.health = health;
        this.speed = speed;
        this.strength = strength;
    }
}
