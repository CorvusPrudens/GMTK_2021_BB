using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjects : MonoBehaviour
{

    public enum RotationAxes {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        CENTER,
        NONE,
    }

    public enum MovementFunction {
        SINE,
        TRIANGLE,
        SQUARE,
        ROUND_SQUARE,
    }

    [System.NonSerialized]
    public float globalSpeed = 1.0f;

    [SerializeField]
    public float localSpeed = 1.0f;
    public bool isAnimating = true;
    
    // [SerializeField, Tooltip("The GameObjects you want to animate.")]
    // GameObject[] targets;

    [Header("Rotation")]
    [SerializeField, Tooltip("The rotation axis of the animated object. It's fine if you just make the axis a cild.")]
    Transform rotationAxis;
    [SerializeField, Tooltip("The individual speed of the gameobject relative to Global Speed")]
    MovementFunction MovementFunction1 = MovementFunction.SINE;
    [SerializeField, Tooltip("Movement function two. These can be blended together.")]
    MovementFunction MovementFunction2 = MovementFunction.SQUARE;
    [SerializeField, Range(0, 1), Tooltip("Amount of blending between functions.")]
    public float functionMorph = 0.5f;

    [SerializeField, Range(0, 1), Tooltip("Function phase (position within the cycle)")]
    float functionPhase = 0f;
    [SerializeField, Range(0, 1), Tooltip("Function range.")]
    float functionRange = 1f;
    [SerializeField, Range(0, 1), Tooltip("Function offset.")]
    float functionOffset = 0f;
    [SerializeField, Range(0.05f, 0.35f), Tooltip("Morph between square and sine for ROUND_SQUARE")]
    float roundedDelta = 0.1f;

    [Header("Bounce")]
    [SerializeField, Tooltip("The individual speed of the gameobject relative to Global Speed")]
    MovementFunction BounceFunction = MovementFunction.ROUND_SQUARE;
    [SerializeField, Range(0, 1), Tooltip("Function phase (position within the cycle)")]
    float bounceFunctionPhase = 0f;
    [SerializeField, Range(0, 1), Tooltip("Function range (in units).")]
    float bounceFunctionRange = 1f;
    [SerializeField, Tooltip("The parent of your object's sprites.")]
    Transform spriteContainer;
    

    float animationProgress = 0;
    float bounceProgress = 0;

    static int TABLE_SIZE = 128;
    static float TWO_PI = Mathf.PI * 2;
    float[] slut = new float[TABLE_SIZE];
    float[] sqlut = new float[TABLE_SIZE];
    float[] tlut = new float[TABLE_SIZE];
    float[] sqrlut = new float[TABLE_SIZE];

    public void Pause()
    {
        isAnimating = false;
    }
    
    public void Play()
    {
        isAnimating = true;
    }

    public void Stop()
    {
        isAnimating = false;
        animationProgress = 0;
        bounceProgress = 0;
    }

    void AnimateObject(int index)
    {
        
    }

    void OnValidate() 
    {
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            float step = (float) i / TABLE_SIZE;
            sqrlut[i] = (2 / Mathf.PI) * Mathf.Atan(Mathf.Sin(TWO_PI * step) / roundedDelta);
        }
    }
    void Start()
    {
        for (int i = 0; i < TABLE_SIZE; i++)
        {
            float step = (float) i / TABLE_SIZE;
            slut[i] = Mathf.Sin(TWO_PI * step);
            sqlut[i] = slut[i] >= 0 ? 1 : 0;
            float accum = 0;
            for (int h = 0; h < 16; h++)
            {
                float sign = (h % 2) == 0 ? 1 : -1;
                float harm = h * 2 + 1;
                accum += sign*Mathf.Sin(TWO_PI * harm * step) / (harm*harm);
            }
            tlut[i] = accum;

            sqrlut[i] = (2 / Mathf.PI) * Mathf.Atan(Mathf.Sin(TWO_PI * step) / roundedDelta);

        }

        if (rotationAxis == null)
        {
            bool found = false;
            int index = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<RotationAxis>() != null)
                {
                    found = true;
                    index = i;
                    break;
                }
            }
            if (found) rotationAxis = transform.GetChild(index);
        }
        
        // doing a little shuffle so the rotation axis
        // becomes this object's parent during gameplay
        if (rotationAxis != null)
        {
            Transform upperParent = transform.parent;
            rotationAxis.SetParent(upperParent);
            transform.SetParent(rotationAxis);
        }
    }

    float GetFunction(MovementFunction type, int index)
    {
        switch (type)
            {   
                default:
                case MovementFunction.SINE:
                    return slut[index];
                case MovementFunction.SQUARE:
                    return sqlut[index];
                case MovementFunction.TRIANGLE:
                    return tlut[index];
                case MovementFunction.ROUND_SQUARE:
                    return sqrlut[index];
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating && rotationAxis != null)
        {
            animationProgress += Time.deltaTime * localSpeed * globalSpeed;
            int index = (int) (((animationProgress + functionPhase) % 1) * TABLE_SIZE);
            float func1 = GetFunction(MovementFunction1, index);
            float func2 = GetFunction(MovementFunction2, index);
            float result = (1 - functionMorph) * func1 + functionMorph * func2;
            float range = 180 * functionRange;
            float offset = 360 * functionOffset;
            rotationAxis.eulerAngles = new Vector3(0, 0, result * range + offset);
        }
        if (isAnimating && spriteContainer != null)
        {
            bounceProgress += Time.deltaTime * localSpeed * globalSpeed;
            int index = (int) (((bounceProgress + bounceFunctionPhase) % 1) * TABLE_SIZE);
            float func = Mathf.Abs(GetFunction(BounceFunction, index));
            float range = bounceFunctionRange;
            spriteContainer.localPosition = new Vector3(0, func * range);
        }
    }
}
