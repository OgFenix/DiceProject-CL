using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card
{
    public int id;
    public string cardName;
    public int manaCost;
    public string cardDisc;
    public Sprite cardImage;
    public Classes cardForClass;
    public List<Tuple<EffectSelector, int>> effects;

    public Card(int id, string cardName, int manaCost, string cardDisc, Sprite cardImage, Classes cardForClass, List<Tuple<EffectSelector, int>> effects)
    {
        this.id = id;
        this.cardName = cardName;
        this.manaCost = manaCost;
        this.cardDisc = cardDisc;
        this.cardImage = cardImage;
        this.cardForClass = cardForClass;
        this.effects = effects;
    }
}
