using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum Stat { health, strength, speed, keys };

public class DisplayPlayerStats : MonoBehaviour
{
    [SerializeField] private Stat statToDisplay;
    private PlayerStats playerStats;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        EventBroker.updateStatsUI += UpdateStat;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateStat();
    }

    void UpdateStat()
    {
        switch (statToDisplay)
        {
            case Stat.health:
                text.text = "Health : " + Mathf.RoundToInt(playerStats.currentHealth);
                break;
            case Stat.strength:
                text.text = "Strength : " + Mathf.RoundToInt(playerStats.maxStats.strength);
                break;
            case Stat.speed:
                text.text = "Speed : " + Mathf.RoundToInt(playerStats.maxStats.speed);
                break;
            case Stat.keys:
                text.text = "Keys : " + playerStats.keys;
                break;
        }
    }
}
