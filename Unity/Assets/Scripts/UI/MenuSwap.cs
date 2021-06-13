using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MonoBehaviour
{
    [SerializeField] private GameObject enable;
    [SerializeField] private GameObject disable;

    public void SwapScreen()
    {
        enable.SetActive(true);
        disable.SetActive(false);
    }
}
