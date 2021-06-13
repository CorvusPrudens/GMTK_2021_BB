using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    public static float Map(float value, float istart, float istop, float ostart, float ostop) 
    {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}

