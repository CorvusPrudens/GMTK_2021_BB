using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    public GameObject[] AnimatedObjects;

    [SerializeField]
    bool isAnimated = true;
    bool prevAnimated = true;

    [SerializeField]
    float globalSpeed = 1.0f;
    float prevSpeed = 1.0f;

    public void Pause()
    {
        foreach (GameObject g in AnimatedObjects)
        {
            g.GetComponent<AnimateObjects>().Pause();
        }
    }

    public void Play()
    {
        foreach (GameObject g in AnimatedObjects)
        {
            g.GetComponent<AnimateObjects>().Play();
        }
    }

    public void Stop()
    {
        foreach (GameObject g in AnimatedObjects)
        {
            g.GetComponent<AnimateObjects>().Stop();
        }
    }

    void Start()
    {
        
    }

    void OnValidate()
    {
        if (isAnimated != prevAnimated)
        {
            foreach (GameObject g in AnimatedObjects)
            {
                g.GetComponent<AnimateObjects>().isAnimating = isAnimated;
            }
            prevAnimated = isAnimated;
        }
        if (globalSpeed != prevSpeed)
        {
             foreach (GameObject g in AnimatedObjects)
            {
                g.GetComponent<AnimateObjects>().globalSpeed = globalSpeed;
            }
            prevSpeed = globalSpeed;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
