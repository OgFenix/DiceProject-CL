using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardsDictionary : MonoBehaviour
{


    //     EffectSelect1 - Amount - EffectSelect2 - Amount 
    private List<Card> cardList = new List<Card>();


    

    /*private void addCard(List<Tuple<EffectSelector, int>> effectsList, string name)
    {
        Tuple<string, List<Tuple<EffectSelector, int>>> curCard = new Tuple<string, List<Tuple<EffectSelector, int>>>(name, effectsList);
        cardList.Add(curCard);
    } */


    // Start is called before the first frame update
    void Start()
    { //ID,Name,ManaCost,Description,Image,Class,EffectList
        cardList.Add(new Card(0,"Attack",1,"Deal 6 damage",Resources.Load<Sprite>("CardSprites/1"),Classes.Warrior,new List<Tuple<EffectSelector, int>>(){Tuple.Create(EffectSelector.Damage,6)})); 
        cardList.Add(new Card(0, "Defense", 1, "Gain 5 Armor", Resources.Load<Sprite>("CardSprites/1"), Classes.Warrior, new List<Tuple<EffectSelector, int>>() { Tuple.Create(EffectSelector.Block, 5) }));
        cardList.Add(new Card(0, "Rage", 1, "Lose 8 Health,Draw 2 Card", Resources.Load<Sprite>("CardSprites/1"), Classes.Warrior, new List<Tuple<EffectSelector, int>>() { Tuple.Create(EffectSelector.DamageSelf, 8),Tuple.Create(EffectSelector.Draw, 2) }));
    }
    public Card InitializeCard(int cardId)
    {
        return cardList[cardId];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
