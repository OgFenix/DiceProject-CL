using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTiming
{
    Immidiate,
    Startofturn,
    Endofturn
}
public class FuncArgs
{
    public int EffectNum;
    public EffectTiming Timing;
    public FuncArgs(int effectNum,EffectTiming timing)
    {
        this.EffectNum = effectNum;
        Timing = timing;
    }
}
