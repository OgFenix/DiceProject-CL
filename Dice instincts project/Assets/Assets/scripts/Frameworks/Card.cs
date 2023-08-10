using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : FrameworkOfObject
{
    public override int id { get; protected set; }
    public string cardName;
    public int manaCost;
    public string cardDisc;
    public Sprite cardImage;
    public Classes cardForClass;
    public int[] shopCostRange;
    public List<FuncArgs> effects;
    public bool isExhaust;

    public Card(int id, string cardName, int manaCost, string cardDisc, Sprite cardImage, Classes cardForClass, int[] shopCostRange, List<FuncArgs> effects, bool isExhaust)
    {
        this.id = id;
        this.cardName = cardName;
        this.manaCost = manaCost;
        this.cardDisc = cardDisc;
        this.cardImage = cardImage;
        this.cardForClass = cardForClass;
        this.shopCostRange = shopCostRange;
        this.effects = effects;
        this.isExhaust = isExhaust;
    }
}
