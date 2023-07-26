using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{


    private List<CardBehaviour> deck = new List<CardBehaviour>();
    private List<RelicBehaviour> relicList = new List<RelicBehaviour>();
    private DiceBehaviour dice;
    private string playerClass;

    public PlayerBehaviour(int health, string characterName, List<CardBehaviour> deck ,List<RelicBehaviour> relicList, DiceBehaviour dice, string playerClass) : base(health, characterName)
    {
        this.deck = deck;
        this.relicList = relicList;
        this.dice = dice;
        this.playerClass = playerClass;
    }
    public void createDeck(string playerClass)
    {
        for (int i = 0; i < 5; i++)
        {


        }
        //add specific class cards

    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour player = this;
        createDeck(player.playerClass);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
