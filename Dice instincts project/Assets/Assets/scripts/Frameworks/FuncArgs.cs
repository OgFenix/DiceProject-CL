using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FuncArgs : EventArgs
{
    public EventHandler<FuncArgs> FuncToRun;
    public int EffectNum;
    public EffectTiming Timing;
    public FuncArgs(EventHandler<FuncArgs> funcToRun, int effectNum,EffectTiming timing)
    {
        FuncToRun = funcToRun;
        EffectNum = effectNum;
        Timing = timing;
    }
}
