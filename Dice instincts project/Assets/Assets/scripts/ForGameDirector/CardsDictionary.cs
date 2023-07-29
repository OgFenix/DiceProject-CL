using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class CardsDictionary : FrameworkDictionary
{
    public override void InitList()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        ListOfObject.Add(new Card(0,"Attack",1,"Deal 6 damage",Resources.Load<Sprite>("CardSprites/0"),Classes.Warrior,new List<Tuple<EffectSelector, int>>(){Tuple.Create(EffectSelector.Damage,6)})); 
        ListOfObject.Add(new Card(1, "Defense", 1, "Gain 5 Armor", Resources.Load<Sprite>("CardSprites/0"), Classes.Warrior, new List<Tuple<EffectSelector, int>>() { Tuple.Create(EffectSelector.Block, 5) }));
        ListOfObject.Add(new Card(2, "Rage", 1, "Lose 8 Health,Draw 2 Card", Resources.Load<Sprite>("CardSprites/0"), Classes.Warrior, new List<Tuple<EffectSelector, int>>() { Tuple.Create(EffectSelector.DamageSelf, 8),Tuple.Create(EffectSelector.Draw, 2) }));
    }
}
