/*using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DiceUpgDictionary : FrameworkDictionary
{
    string Path = "DiceUpgSprites/";
    private void AddToList(int ID, string name, string disc, bool isTorandomlyAssign, List<int> dicefaces, List<FuncArgs> Specialeffects)
    {
        ListOfObject.Add(new DiceUpg(ID, name, disc, Resources.Load<Sprite>(Path + $"{ID}"), isTorandomlyAssign, dicefaces, Specialeffects));
    }
    //string ScriptPath = "Assets.Assets.scripts.Relics.";
    string ScriptPath = string.Empty;
    public override void InitList()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        //AddToList(0, "Namaim", "Namaim is the man!", Classes.Warrior, new List<FuncArgs>(){ new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 4, EffectTiming.Startofturn)});
        AddToList(0,"Rising Up!","Choose 2 dicefaces to gain 1,2", false, new List<int>() {1,2},new List<FuncArgs>() { });
        AddToList(1, "Money Mania", "Each time you land on a coin tile, add 2 to a random diceface", false, new List<int>() { }, new List<FuncArgs>() {new FuncArgs(2,1,EffectTiming.LandingOnCoinTile)});
    }
} */
