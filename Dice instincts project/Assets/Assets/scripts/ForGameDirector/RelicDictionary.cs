using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDictionary : FrameworkDictionary
{
    string Path = "RelicSprites/";
    private void AddToList(int ID, string name, string disc, Classes cardClass, List<FuncArgs> effects)
    {
        ListOfObject.Add(new Relic(ID, name, disc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, effects));
    }
    //string ScriptPath = "Assets.Assets.scripts.Relics.";
    string ScriptPath = string.Empty;
    public override void InitList()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        //AddToList(0, "Namaim", "Namaim is the man!", Classes.Warrior, new List<FuncArgs>(){ new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 4, EffectTiming.Startofturn)});
        AddToList(0, "Spinach", "Gain 1 strength for each 2 rolled in your last roll", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnSelf,boardManager.GetCurrentDiceRoll,2,0, EffectTiming.EnterCombat,Status.strength) });
    }
}
