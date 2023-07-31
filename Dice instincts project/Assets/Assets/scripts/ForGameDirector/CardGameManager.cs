using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject cardContainer;
    
    [SerializeField]
    TextMeshProUGUI deckAmount;

    [SerializeField]
    private List<GameObject> deck = new List<GameObject>();

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private PlayerBehaviour player;

    private List<GameObject> curFightDeck;

    private List<GameObject> discardPile = new List<GameObject>();
    OverallGameManager gameManager;

    private void copyList(List<GameObject> curFightDeck, List<GameObject> discardPile)
    {
        foreach(var card in discardPile)
        {
            curFightDeck.Add(card);
        }
        discardPile.Clear();
    }
    private void DrawCards(FuncArgs args)
    {
        for (int i = 0; i < args.EffectNum; i++) {
            if (curFightDeck.Count == 0 && discardPile.Count > 0)
                copyList(curFightDeck, discardPile);
            else if (curFightDeck.Count > 0)
            {
                curFightDeck[0].transform.SetParent(hand.transform);
                curFightDeck[0].SetActive(true);
            }
            else
            {
                //logic for no more cards
                Debug.Log("Can't Draw! no more cards");
            }
        }

    }
    private void DealDamage(FuncArgs args)
    {
        bool isWeak = false;
        int curEffectNum = args.EffectNum;
        foreach(var status in args.character.statusesList)
        {
            if(status.status == Status.weak)
                isWeak = true;

        }
        if (isWeak)
            curEffectNum = (int)(curEffectNum * 0.75f);

        if (args.character.block >= curEffectNum)
            args.character.block -= curEffectNum;
        else
        {
            curEffectNum -= args.character.block;
            args.character.block = 0;
            args.character.health -= curEffectNum;
        }
    }
    private void ApplyStatus(FuncArgs args)
    {
        for(int i = 0; i< args.character.statusesList.Count; i++)
        {
            if (args.character.statusesList[i].status == args.status)
            {
                args.character.statusesList[i].amountOfTurns += args.EffectNum;
                return;
            }
        }
        args.character.statusesList.Add(new CharacterStatus(args.status, args.EffectNum));
    }
    private void GainBlock(FuncArgs args)
    {
        args.character.block += args.EffectNum;
    }
    private void DamageSelf(FuncArgs args)
    {
        args.character = player;
        DealDamage(args);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        curFightDeck = ListRandomizer.Randomize(gameManager.deck);
    }

    // Update is called once per frame
    void Update()
    {
        deckAmount.text = deck.Count.ToString();
    }

    public void AAAAA(FuncArgs Args)
    {

    }
}
