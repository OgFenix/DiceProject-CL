using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CardsDictionary : MonoBehaviour
{


    //     EffectSelect1 - Amount - EffectSelect2 - Amount 
    List<Tuple<string, List<Tuple<EffectSelector, int>>>> cardIDs = new List<Tuple<string, List<Tuple<EffectSelector, int>>>>();


    

    private void addCard(List<Tuple<EffectSelector, int>> effectsList, string name)
    {
        Tuple<string, List<Tuple<EffectSelector, int>>> curCard = new Tuple<string, List<Tuple<EffectSelector, int>>>(name, effectsList);
        cardIDs.Add(curCard);
    }


    // Start is called before the first frame update
    void Start()
    {
        addCard(new List<Tuple<EffectSelector, int>>()
        {
            new Tuple<EffectSelector, int>(EffectSelector.Damage, 6)
        }, "Attack");
        addCard(new List<Tuple<EffectSelector, int>>()
        {
            new Tuple<EffectSelector, int>(EffectSelector.Block, 5)
        }, "Defend");
    }
    public Tuple<string, List<Tuple<EffectSelector, int>>> InitializeCard(string cardName)
    {
        foreach(var cardID in cardIDs)
        {
            if(cardID.Item1 == cardName)
            {
                return cardID;
            }
        }
        return null;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
