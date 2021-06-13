using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private void Awake()
    {
        EventBroker.resetGame += DoIt;
    }

    private void DoIt()
    {
        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMenu");

    }
}
