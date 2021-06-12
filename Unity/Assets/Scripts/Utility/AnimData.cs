using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimData : ScriptableObject
{

    // public static void UpdateArray(GameObject[] array, int newSize)
    // {
    //     if (newSize > array.Length)
    //     {
    //         System.Array.Resize(ref array, newSize);
    //     }
    // }
    // public static void UpdateArray(int[] array, int newSize)
    // {
    //     if (newSize > array.Length)
    //     {
    //         System.Array.Resize(ref array, newSize);
    //     }
    // }
    // public static void UpdateArray(float[] array, int newSize)
    // {
    //     if (newSize > array.Length)
    //     {
    //         System.Array.Resize(ref array, newSize);
    //     }
    // }
    public enum RotationAxes {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        CENTER,
    }

    public enum MovementFunction {
        SINE,
        TRIANGLE,
        SQUARE,
    }

    public GameObject target;
    public RotationAxes rotationAxis;
    public float speedMultiplier = 1.0f;
    public MovementFunction movementFunction1;
    public MovementFunction movementFunction2;

    [SerializeField]
    public float functionMorph = 0.5f;
}
