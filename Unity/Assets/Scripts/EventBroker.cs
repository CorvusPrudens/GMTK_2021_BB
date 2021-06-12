using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBroker
{
    public static event Action<Stats> applyPlayerStats;

    public static void CallApplyPLayerStats(Stats statsToApply)
    {
        applyPlayerStats?.Invoke(statsToApply);
    }
}
