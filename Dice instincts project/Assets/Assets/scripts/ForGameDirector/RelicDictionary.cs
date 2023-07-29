using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDictionary : FrameworkDictionary
{
    //string ScriptPath = "Assets.Assets.scripts.Relics.";
    string ScriptPath = string.Empty;
    public override void InitList()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        ListOfObject.Add(new Relic(0,"Namaim","Namaim is the man!",Resources.Load<Sprite>("RelicSprites/0"),Classes.Warrior, ScriptPath + "Relics_0"));
    }
}
