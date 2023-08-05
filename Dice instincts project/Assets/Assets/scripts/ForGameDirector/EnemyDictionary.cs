using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDictionary : FrameworkDictionary
{
    string Path = "EnemySprites/";
    private void AddToList(int ID, string name, int health, List<FuncArgs> effects)
    {
        ListOfObject.Add(new Enemy(ID, name, health,Resources.Load<Sprite>(Path + $"{ID}"), effects));
    }
    public override void InitList()
    {
        //ID,Name,Health,Image,Attack,EffectsList
        AddToList(0, "Mondo", 15, new List<FuncArgs>() { new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnSelf, 8, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DealDamage, cardGameManager.EffectOnPlayer, 8, EffectTiming.Immidiate)});
        AddToList(1, "Mondo", 25, new List<FuncArgs>() { new FuncArgs(cardGameManager.GainBlock, cardGameManager.EffectOnSelf, 10, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DealDamage, cardGameManager.EffectOnPlayer, 5, EffectTiming.Immidiate) });
        AddToList(2, "Mondo", 10, new List<FuncArgs>() { new FuncArgs(cardGameManager.GainBlock, cardGameManager.EffectOnSelf, 5, EffectTiming.Immidiate), new FuncArgs(cardGameManager.DealDamage, cardGameManager.EffectOnPlayer, 15, EffectTiming.Immidiate) });
    }

}
