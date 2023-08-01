using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class CardsDictionary : FrameworkDictionary
{
    string Path = "RelicSprites/";
    private void AddToList(int ID,string name,int manaCost,string disc,Classes cardClass, List<FuncArgs> effects)
    {
        ListOfObject.Add(new Card(ID, name, manaCost, disc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, effects));
    }
    public override void InitList()
    {
        //ID,Name,ManaCost,Description,Image,Class,EffectList
        AddToList(0, "Attack", 1, "Deal 6 damage", Classes.Warrior, new List<FuncArgs>() {new FuncArgs(cardGameManager.DealDamage,cardGameManager.EffectOnEnemyTargeted,6, EffectTiming.Immidiate)});
        AddToList(1, "Defense", 1, "Gain 5 armor", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 5, EffectTiming.Immidiate)});
        AddToList(2, "Rage", 1, "Lose 8 Health,Draw 2 Cards", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.DamagePlayer,cardGameManager.EffectOnPlayer, 8, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DrawCards,cardGameManager.EffectOnPlayer, 2, EffectTiming.Immidiate)});
    }
}
