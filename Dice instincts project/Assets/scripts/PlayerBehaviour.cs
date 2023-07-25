using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{


    private List<CardBehaviour> deck = new List<CardBehaviour>();
    private List<RelicBehaviour> relicList = new List<RelicBehaviour>();
    private DiceBehaviour dice;

    public PlayerBehaviour(int health, string characterName, List<CardBehaviour> deck ,List<RelicBehaviour> relicList, DiceBehaviour dice) : base(health, characterName)
    {
        this.deck = deck;
        this.relicList = relicList;
        this.dice = dice;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
