using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FuncArgs : EventArgs
{
    
    public EventHandler<FuncArgs> FuncToRun;
    public int EffectNum;
    public EffectTiming Timing;
    public EventHandler<FuncArgs> TargetTypeFunc;
    public CharacterBehaviour character = null;
    public Status status;
    public FuncArgs(EventHandler<FuncArgs> funcToRun, EventHandler<FuncArgs> targetTypeFunc, int effectNum, EffectTiming timing)
    {
        TargetTypeFunc = targetTypeFunc;
        FuncToRun = funcToRun;
        EffectNum = effectNum;
        Timing = timing;
    }
    public FuncArgs(EventHandler<FuncArgs> funcToRun, EventHandler<FuncArgs> targetTypeFunc, int effectNum, EffectTiming timing, Status status) 
        : this(funcToRun,targetTypeFunc,effectNum,timing)
    {
        this.status = status;
    }
}
