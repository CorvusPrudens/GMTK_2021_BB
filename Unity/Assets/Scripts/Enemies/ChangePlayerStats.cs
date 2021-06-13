using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerStats : MonoBehaviour
{
    public Stats statsToApply;

    public void ApplyStatsToPlayer()
    {
        AkSoundEngine.PostEvent("Player_CollectSoul", this.gameObject);

        EventBroker.CallApplyPLayerStats(statsToApply);
    }
}
