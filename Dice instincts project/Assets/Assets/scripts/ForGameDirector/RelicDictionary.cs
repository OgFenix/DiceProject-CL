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
        AddToList(0, "Namaim", "Namaim is the man!", Classes.Warrior, new List<FuncArgs>(){ new FuncArgs(cardGameManager.GainBlock, 4, EffectTiming.Startofturn)});
    }
}
