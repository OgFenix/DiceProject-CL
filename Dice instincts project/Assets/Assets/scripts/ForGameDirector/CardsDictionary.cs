using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class CardsDictionary : FrameworkDictionary
{
    private void AddToList(int ID,string name,int manaCost,string disc,string spritePath,Classes cardClass, List<Tuple<Action<FuncArgs>, FuncArgs>> effects)
    {
        ListOfObject.Add(new Card(ID, name, manaCost, disc, Resources.Load<Sprite>(spritePath + $"{ID}"), cardClass, effects));
    }
    public override void InitList()
    {
        //ID,Name,ManaCost,Description,Image,Class,EffectList
        AddToList(0, "Attack", 1, "Deal 6 damage", "CardSprites/", Classes.Warrior, new List<Tuple<Action<FuncArgs>, FuncArgs>>() { new Tuple<Action<FuncArgs>, FuncArgs>(cardGameManager.AAAAA, new FuncArgs(6,EffectTiming.Immidiate))});
        AddToList(1, "Defense", 1, "Gain 5 armor", "CardSprites/", Classes.Warrior, new List<Tuple<Action<FuncArgs>, FuncArgs>>() { new Tuple<Action<FuncArgs>, FuncArgs>(cardGameManager.AAAAA, new FuncArgs(5, EffectTiming.Immidiate)) });
        AddToList(2, "Rage", 1, "Lose 8 Health,Draw 2 Cards", "CardSprites/", Classes.Warrior, new List<Tuple<Action<FuncArgs>, FuncArgs>>() { new Tuple<Action<FuncArgs>, FuncArgs>(cardGameManager.AAAAA, new FuncArgs(8, EffectTiming.Immidiate)), new Tuple<Action<FuncArgs>, FuncArgs>(cardGameManager.AAAAA, new FuncArgs(2, EffectTiming.Immidiate))});
    }
}
