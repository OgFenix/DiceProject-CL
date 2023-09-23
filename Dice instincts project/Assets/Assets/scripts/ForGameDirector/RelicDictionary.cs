using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDictionary : FrameworkDictionary
{
    enum UpgradeFor
    {
        Cardgame,
        Boardgame
    }
    string Path = "RelicSprites/";
    private void AddToList(UpgradeFor forThis,int ID, string name, string disc, Classes cardClass, int[] shopCostRange, List<FuncArgs> effects)
    {
        if(forThis == UpgradeFor.Cardgame)
            ListOfObject.Add(new Relic(ID, name, disc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, shopCostRange, effects));
        else
            ListOfObject2.Add(new Relic(ID, name, disc, Resources.Load<Sprite>(Path + $"{ID}"), cardClass, shopCostRange, effects));
    }
    //string ScriptPath = "Assets.Assets.scripts.Relics.";
    string ScriptPath = string.Empty;
    public override void InitList()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        //AddToList(0, "Namaim", "Namaim is the man!", Classes.Warrior, new List<FuncArgs>(){ new FuncArgs(cardGameManager.GainBlock,cardGameManager.EffectOnPlayer, 4, EffectTiming.Startofturn)});
        //Card Game Upgrades
        AddToList(UpgradeFor.Cardgame,0, "Spinach", "Gain 1 strength for each 2 rolled in your last roll", Classes.Warrior,new int[] {27,41}, new List<FuncArgs>() { new FuncArgs(cardGameManager.ApplyStatus, cardGameManager.EffectOnSelf,boardManager.GetCurrentDiceRoll,2,0, EffectTiming.EnterCombat,Status.strength) });
        //Board Upgrades
        AddToList(UpgradeFor.Cardgame,1, "Rise up!", "Choose 3 dicefaces to gain 1,2,3", Classes.Warrior, new int[] { 27, 41 }, new List<FuncArgs>() {new FuncArgs(boardManager.ActivateDiceUpgChoice,new List<int>(){ 1, 2, 3 },false, EffectTiming.Immidiate)});
    }
}
