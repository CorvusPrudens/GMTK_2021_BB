using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum Stat { health, strength, speed };

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
                text.text = "Health :" + playerStats.currentHealth;
                break;
            case Stat.strength:
                text.text = "Strength :" + playerStats.maxStats.strength;
                break;
            case Stat.speed:
                text.text = "Speed :" + playerStats.maxStats.speed;
                break;
        }
    }
}
