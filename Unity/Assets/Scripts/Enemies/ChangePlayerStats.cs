using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerStats : MonoBehaviour
{
    public Stats statsToApply;

    public void ApplyStatsToPlayer()
    {
        EventBroker.CallApplyPLayerStats(statsToApply);
    }
}
