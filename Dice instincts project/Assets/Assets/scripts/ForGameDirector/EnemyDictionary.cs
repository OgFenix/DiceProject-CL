using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDictionary : FrameworkDictionary
{
    

    public override void InitList()
    {
        //IDs 100-199
        //ID,Name,Health,Image,Attack,EffectsList
        ListOfObject.Add(new Enemy(0, "Mondo", 15, Resources.Load<Sprite>("EnemySprites/0"), 4, new List<Tuple<EnemiesEffectSelector, int>> { Tuple.Create(EnemiesEffectSelector.Attack, 5), Tuple.Create(EnemiesEffectSelector.strength,2) }));
    }

}
