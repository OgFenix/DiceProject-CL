using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public delegate void GameEvent(EffectTiming timing);

public class OverallGameManager : MonoBehaviour
{
    public event GameEvent EnterCombatEvent;
    [SerializeField]
    GameObject scrollContainer;
    [SerializeField]
    Board_CameraDrag _board_CameraDrag;
    [SerializeField]
    TextMeshProUGUI deckButtonText;
    [SerializeField]
    private GameObject EnemyContainer;
    [SerializeField]
    private CardGameManager cardGameManager;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private GameObject Board;
    [SerializeField]
    private GameObject BoardInCanvas;
    [SerializeField]
    private GameObject CardGame;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject relicPrefab;
    [SerializeField] 
    private GameObject EnemyPrefab;
    [SerializeField]
    private TextMeshProUGUI deckAmount;
    [SerializeField]
    public GameObject cardContainer;
    [SerializeField]
    public GameObject RelicContainer;
    List<int> startingDeckIDs = new List<int>() { 0, 0, 1, 1, 2, 3, 4, 5};
    [SerializeField]
    public List<GameObject> deck;
    public EnemyMovement EnemyInCombat;
    private EnemyBehaviour CurEncounter;

    public GameObject getCardContainer() { return cardContainer; }

    public void EnterCombat(EnemyMovement enemyMovement, bool IsFromEnemy = false)
    {
        EnterCombatEvent?.Invoke(EffectTiming.EnterCombat);
        _board_CameraDrag.isInBoardState = false;
        cardGameManager.player.CurManaToMaxMana();
        cardGameManager.ClearDiscardPile();
        cardGameManager.CardsFromHandToContainer();
        cardGameManager.curFightDeck = ListFunctions<GameObject>.Randomize(deck).ToList();
        boardManager.IsInCombat = true;
        CardGame.SetActive(true);
        EnemyInCombat = enemyMovement;
        CurEncounter = Instantiate(EnemyPrefab, EnemyContainer.transform).GetComponent<EnemyBehaviour>();
        CurEncounter.CreateEnemy(enemyMovement.EnemyID);
        BoardInCanvas.SetActive(false);
        Board.SetActive(false);
        cardGameManager.TurnStart();
    }
    public void CombatWon()
    {
        _board_CameraDrag.isInBoardState = true;
        cardGameManager.player.RemoveAllStatuses();
        cardGameManager.player.statusesList.Clear();
        cardGameManager.player.CurManaToMaxMana();
        cardGameManager.ClearDiscardPile();
        cardGameManager.ClearExhaustPile();
        cardGameManager.CardsFromHandToContainer();
        cardGameManager.player.boardHealthText.text = cardGameManager.player.CurHealthText.text;
        boardManager.IsInCombat = false;
        boardManager.enemies.Remove(EnemyInCombat);
        Destroy(EnemyInCombat.gameObject);
        Destroy(CurEncounter.gameObject);
        Board.SetActive(true);
        BoardInCanvas.SetActive(true);
        CardGame.SetActive(false);
        deck = ListFunctions<GameObject>.SortListByName(deck).ToList();
        boardManager.healthBar.SetHealth(cardGameManager.player.health);
        
    }
    public void CombatLost()
    {

    }
    public void ActivateEffect(object sender, FuncArgs args)
    {
        if (args.modnum != 0)
            if (args.ForEachUpTo != 0)
                args.EffectNum = math.min((int)math.floor(args.GetEnvelopeNumber() / args.modnum), args.ForEachUpTo);
            else
                args.EffectNum = (int)math.floor(args.GetEnvelopeNumber() / args.modnum);
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
            case EffectTiming.EnterCombat:
                EnterCombatEvent += action;
                break;
        }
    }

    public void UnSubscribeToReleventEvent(EffectTiming trigger, GameEvent action)
    {
        switch (trigger)
        {
            case EffectTiming.Startofturn:
                cardGameManager.StartOfTurn -= action;
                break;
            case EffectTiming.Endofturn:
                cardGameManager.EndOfTurn -= action;
                break;
            case EffectTiming.EnterCombat:
                EnterCombatEvent -= action;
                break;
        }
    }
    private void initializeDeck()
    {
        GameObject curCard;
        foreach (var cardID in startingDeckIDs)
        {
            curCard = Instantiate(cardPrefab);
            curCard.GetComponent<CardBehaviour>().Create(cardID, false);
            curCard.transform.SetParent(cardContainer.transform, false);
            curCard.SetActive(false);
            curCard.name = curCard.GetComponent<CardBehaviour>().cardName;
            deck.Add(curCard);
        }
        cardGameManager.curFightDeck = deck;
        deckButtonText.text = cardGameManager.curFightDeck.Count.ToString();
        
    }
    private void updateDeckAmount()
    {
        deckButtonText.text = deck.Count.ToString();
    }
    public void AddCardToDeck(int id, bool isUpgraded)
    {
        CardBehaviour newCard = Instantiate(cardPrefab).GetComponent<CardBehaviour>();
        newCard.Create(id, isUpgraded);
        newCard.transform.SetParent(cardContainer.transform,false);
        deck.Add(newCard.gameObject);
        newCard.name = newCard.GetComponent<CardBehaviour>().cardName;
        newCard.gameObject.SetActive(false);
        deckAmount.text = deck.Count.ToString();
        updateDeckAmount();
    }
    public void AddRelic(int id)
    {
        RelicBehaviour newRelic = Instantiate(relicPrefab).GetComponent<RelicBehaviour>();
        newRelic.Create(id);
        newRelic.transform.SetParent(RelicContainer.transform, false);
    }
    // Start is called before the first frame update

    public void RemoveCardFromDeck(GameObject card)
    {
        deck.Remove(card);
        Destroy(card.gameObject);
        updateDeckAmount();

    }
    void Start()
    {
        initializeDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
