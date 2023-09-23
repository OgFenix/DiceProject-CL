using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class FuncArgs : EventArgs
{
    
    public EventHandler<FuncArgs> FuncToRun;
    public EventHandler<FuncArgs> TargetTypeFunc;
    public List<int> Dicefaces;
    public bool IsToSetTo;
    public int EffectNum;
    public Func<int> GetEnvelopeNumber = null;
    public int modnum = 0;
    public int ForEachUpTo = 0;
    public EffectTiming Timing;
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
    public FuncArgs(EventHandler<FuncArgs> funcToRun, EventHandler<FuncArgs> targetTypeFunc, Func<int> getEnvelopeNumber, int modnum, int forEachUpTo, EffectTiming timing)
    {
        FuncToRun = funcToRun;
        TargetTypeFunc = targetTypeFunc;
        this.GetEnvelopeNumber = getEnvelopeNumber;
        this.modnum = modnum;
        ForEachUpTo = forEachUpTo;
        Timing = timing;
    }
    public FuncArgs(EventHandler<FuncArgs> funcToRun, EventHandler<FuncArgs> targetTypeFunc, Func<int> getEnvelopeNumber, int modnum, int forEachUpTo, EffectTiming timing, Status status)
        :this(funcToRun,targetTypeFunc, getEnvelopeNumber, modnum,forEachUpTo, timing)
    {
        this.status = status;
    }
    public FuncArgs(EventHandler<FuncArgs> funcToRun, List<int> diceFaces,bool isToSetTo, EffectTiming timing)
    {
        IsToSetTo = isToSetTo;
        TargetTypeFunc = funcToRun;
        Dicefaces = diceFaces;
        Timing = timing;
    }
}
