using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class CardsDictionary : FrameworkDictionary
{
    string Path = "RelicSprites/";
    private void AddToList(int ID,string name,int manaCost,string desc,Classes cardClass, List<FuncArgs> effects)
    {
        ListOfObject.Add(new Card(ID, name, manaCost, desc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, effects));
    }
    public override void InitList()
    {
        //ID,Name,ManaCost,Description,Image,Class,EffectList
        AddToList(0, "Attack", 1, "Deal 6 damage", Classes.Warrior, new List<FuncArgs>() {new FuncArgs(cardGameManager.DealDamage,cardGameManager.EffectOnEnemyTargeted,6, EffectTiming.Immidiate)});
        AddToList(1, "Defense", 1, "Gain 5 armor", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 5, EffectTiming.Immidiate)});
        AddToList(2, "Rage", 1, "Lose 8 Health,Draw 2 Cards", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.DealDamage,cardGameManager.EffectOnPlayer, 8, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DrawCards,cardGameManager.EffectOnPlayer, 2, EffectTiming.Immidiate)});
        AddToList(3, "Weak", 0, "Apply 2 weak to an enemy", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnEnemyTargeted, 2, EffectTiming.Immidiate, Status.weak) });
        AddToList(4, "Poison", 2, "Apply 8 poison to an enemy", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnEnemyTargeted, 8, EffectTiming.Immidiate, Status.poison) });
        AddToList(5, "Power Up!", 1, "Gain 2 strength\nExhaust", Classes.Warrior, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnSelf, 2, EffectTiming.Immidiate, Status.strength) });

    }
}
