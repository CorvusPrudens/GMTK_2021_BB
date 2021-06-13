using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void HoverSfx()
    {
        AkSoundEngine.PostEvent("Hover", this.gameObject);
    }

    public void PressSfx()
    {
        AkSoundEngine.PostEvent("Press", this.gameObject);
    }
}
