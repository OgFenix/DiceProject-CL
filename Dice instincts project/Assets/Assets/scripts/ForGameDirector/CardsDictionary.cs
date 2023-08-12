using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class CardsDictionary : FrameworkDictionary
{
    string Path = "RelicSprites/";
    private void AddToList(int ID,string name,int manaCost,string desc,Classes cardClass, int[] shopCostRange, List<FuncArgs> effects, bool isExhaust)
    {
        ListOfObject.Add(new Card(ID, name, manaCost, desc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, shopCostRange, effects, isExhaust));
    }
    public override void InitList()
    {
        //ID,Name,ManaCost,Description,Image,Class,EffectList
        AddToList(0, "Attack", 1, "Deal 6 damage", Classes.Warrior, new int[] {5,11}, new List<FuncArgs>() {new FuncArgs(cardGameManager.DealDamage,cardGameManager.EffectOnEnemyTargeted,6, EffectTiming.Immidiate)}, false);
        AddToList(1, "Defense", 1, "Gain 5 armor", Classes.Warrior, new int[] {5,11}, new List<FuncArgs>() {new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 5, EffectTiming.Immidiate)}, false);
        AddToList(2, "Rage", 1, "Lose 8 Health,Draw 2 Cards", Classes.Warrior, new int[] {15, 20}, new List<FuncArgs>() { new FuncArgs(cardGameManager.DealDamage,cardGameManager.EffectOnPlayer, 8, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DrawCards,cardGameManager.EffectOnPlayer, 2, EffectTiming.Immidiate)}, false);
        AddToList(3, "Weak", 0, "Apply 2 weak to an enemy", Classes.Warrior, new int[] {13,19}, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnEnemyTargeted, 2, EffectTiming.Immidiate, Status.weak) }, false);
        AddToList(4, "Poison", 2, "Apply 8 poison to an enemy", Classes.Warrior, new int[] {41,53}, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnEnemyTargeted, 8, EffectTiming.Immidiate, Status.poison) }, false);
        AddToList(5, "Power Up!", 1, "Gain 2 strength\nExhaust", Classes.Warrior, new int[] {31,43},new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnSelf, 2, EffectTiming.Immidiate, Status.strength) }, true);

    }
}
