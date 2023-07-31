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
    public CharacterBehaviour character = null;
    public Status status;
    public FuncArgs(int effectNum, EffectTiming timing)
    {
        this.EffectNum = effectNum;
        Timing = timing;
    }
    public FuncArgs(int effectNum, EffectTiming timing, Status status)
    {
        this.EffectNum = effectNum;
        Timing = timing;
        this.status = status;
    }
}
