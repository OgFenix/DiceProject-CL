using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void GameEvent(EffectTiming timing);

public class OverallGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject overallCardContainer;
    [SerializeField]
    private CardGameManager cardGameManager;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private TextMeshProUGUI deckAmount;
    [SerializeField]
    private GameObject cardContainer;
    List<int> startingDeckIDs = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 1, 2 };
    [SerializeField]
    public List<GameObject> deck;
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
            curCard.transform.SetParent(overallCardContainer.transform, false);
            curCard.SetActive(false);
            deck.Add(curCard);
        }
        cardGameManager.curFightDeck = deck;
    }
    
    public void AddCardToDeck(int id)
    {
        CardBehaviour newCard = Instantiate(cardPrefab).GetComponent<CardBehaviour>();
        newCard.CreateCard(id);
        newCard.transform.SetParent(cardContainer.transform);
        deck.Add(newCard.gameObject);
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
