using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDictionary : FrameworkDictionary
{

    private void AddToList(int ID, string name, int manaCost, string disc, string spritePath, Classes cardClass, List<Tuple<Action<FuncArgs>, FuncArgs>> effects)
    {
        ListOfObject.Add(new Card(ID, name, manaCost, disc, Resources.Load<Sprite>(spritePath + $"{ID}"), cardClass, effects));
    }
    public override void InitList()
    {
        //ID,Name,Health,Image,Attack,EffectsList
        ListOfObject.Add(new Enemy(0, "Mondo", 15, Resources.Load<Sprite>("EnemySprites/0"), 4, new List<Tuple<EnemiesEffectSelector, int>> { Tuple.Create(EnemiesEffectSelector.Attack, 5), Tuple.Create(EnemiesEffectSelector.strength,2) }));
    }

}
