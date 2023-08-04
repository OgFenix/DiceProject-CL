using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void GameEvent(EffectTiming timing);

public class OverallGameManager : MonoBehaviour
{
    [SerializeField]
    private CardGameManager cardGameManager;
    [SerializeField]
    private GameObject Board;
    [SerializeField]
    private GameObject BoardInCanvas;
    [SerializeField]
    private GameObject CardGame;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private TextMeshProUGUI deckAmount;
    [SerializeField]
    private GameObject cardContainer;
    List<int> startingDeckIDs = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 1, 2, 3};
    [SerializeField]
    public List<GameObject> deck;

    public void EnterCombat(EnemyMovement enemy, bool IsFromEnemy = false)
    {
        Board.SetActive(false);
        BoardInCanvas.SetActive(false);
        CardGame.SetActive(true);
    }
    public void ImmidateActivate(object sender, FuncArgs args)
    {
        args.TargetTypeFunc(sender,args);
    }

    public void SubscribeToReleventEvent(EffectTiming trigger, GameEvent action)
    {
        switch (trigger)
        {
            case EffectTiming.Startofturn:
                cardGameManager.StartOfTurn += action;
                break;
            case EffectTiming.Endofturn:
                cardGameManager.EndOfTurn += action;
                break;
        }
    }
    private void initializeDeck()
    {
        GameObject curCard;
        foreach (var cardID in startingDeckIDs)
        {
            curCard = Instantiate(cardPrefab);
            curCard.GetComponent<CardBehaviour>().CreateCard(cardID);
            curCard.transform.SetParent(cardContainer.transform, false);
            curCard.SetActive(false);
            curCard.name = curCard.GetComponent<CardBehaviour>().cardName;
            deck.Add(curCard);
        }
        cardGameManager.curFightDeck = deck;
    }
    
    public void AddCardToDeck(int id)
    {
        CardBehaviour newCard = Instantiate(cardPrefab).GetComponent<CardBehaviour>();
        newCard.CreateCard(id);
        newCard.transform.SetParent(cardContainer.transform,false);
        deck.Add(newCard.gameObject);
        newCard.name = newCard.GetComponent<CardBehaviour>().cardName;
        newCard.gameObject.SetActive(false);
        deckAmount.text = deck.Count.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        initializeDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
