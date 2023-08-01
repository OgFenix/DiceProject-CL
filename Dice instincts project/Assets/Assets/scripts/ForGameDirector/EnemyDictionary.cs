using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDictionary : FrameworkDictionary
{
    string Path = "RelicSprites/";
    private void AddToList(int ID, string name, int health, int attack, List<FuncArgs> effects)
    {
        ListOfObject.Add(new Enemy(ID, name, health,Resources.Load<Sprite>(Path + $"{ID}"), attack, effects));
    }
    public override void InitList()
    {
        //ID,Name,Health,Image,Attack,EffectsList
        AddToList(0, "Mondo", 15,4, new List<FuncArgs>() { new FuncArgs(cardGameManager.DamagePlayer, 8, EffectTiming.Immidiate)});
    }

}
