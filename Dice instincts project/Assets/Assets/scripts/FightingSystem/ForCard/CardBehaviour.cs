using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    private PlayerBehaviour Player;
    private OverallGameManager overallGameManager;
    private CardGameManager cardGameManager;
    private bool hasBeenPlayed;
    private bool hasOvertimeEffect;
    private int handIndex;
    private Card thisCard;
    private CardsDictionary cardsDictionary;
    private bool IsCardInit = false;

    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI manacostText;
    public TextMeshProUGUI descriptionText;
    public Image cardBorders;
    public Image cardImage;

    public int id = -1;
    public string cardName;
    public int cost;
    public string cardDisc;
    public Sprite cardSprite;
    public Classes cardForClass;
    public List<FuncArgs> effects;

    public void activateCard()
    {
        
    }

    private void MoveToDiscardPile()
    {
        cardGameManager.discardPile.Add(this.gameObject);
        cardGameManager.DiscardAmount.text = cardGameManager.discardPile.Count.ToString();
    }

    
    private void GetChildrenComponents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            switch(childObject.tag)
            {
                case "Card_Name":
                    cardNameText = childObject.GetComponent<TextMeshProUGUI>();
                    break;
                case "CardDescription":
                    descriptionText = childObject.GetComponent<TextMeshProUGUI>();
                    break;
                case "ManaCost":
                    manacostText = childObject.GetComponent<TextMeshProUGUI>();
                    break;
                case "CardImage":
                    cardImage = childObject.GetComponent<Image>();
                    break;
                case "Card":
                    cardBorders = childObject.GetComponent<Image>();
                    break;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!IsCardInit)
        {
            id = UnityEngine.Random.Range(0, 3); // make it take from ids in deck later
            CreateCard(id);
        }
    }

    public void CreateCard(int id)
    {
        GetChildrenComponents();
        Player = GameObject.Find("PlayerStats")?.GetComponent<PlayerBehaviour>();
        GameObject gamedirector = GameObject.Find("GameDirector");
        overallGameManager = gamedirector.GetComponent<OverallGameManager>();
        cardGameManager = gamedirector.GetComponent<CardGameManager>();
        cardsDictionary = gamedirector.GetComponent<CardsDictionary>();
        Player = cardGameManager.player;
        thisCard = (Card)cardsDictionary.InitializeByID(id);
        //creating card from thiscard
        this.id = thisCard.id;
        cardName = thisCard.cardName;
        cost = thisCard.manaCost;
        cardDisc = thisCard.cardDisc;
        cardSprite = thisCard.cardImage;
        cardForClass = thisCard.cardForClass;
        effects = thisCard.effects;
        cardImage.sprite = cardSprite;
        cardNameText.text = cardName;
        manacostText.text = cost.ToString();
        descriptionText.text = cardDisc;
        IsCardInit = true;
    }

    void ActivateEffect(EffectTiming Timing)
    {
        foreach (var effect in effects)
            if (effect.Timing == Timing)
                effect.TargetTypeFunc(this,effect);
    }

    public void PlayCard(GameObject TargetedEnemy)
    {
        
        hasOvertimeEffect = false;
        foreach (var effect in effects)
        {
            if(TargetedEnemy != null)
                effect.character = TargetedEnemy.GetComponent<EnemyBehaviour>();
            if (effect.Timing == EffectTiming.Immidiate)
                overallGameManager.ImmidateActivate(Player.gameObject, effect);
            else
            {
                hasOvertimeEffect = true;
                overallGameManager.SubscribeToReleventEvent(effect.Timing, ActivateEffect);
            }
        }
        this.gameObject.SetActive(false);
        Player.UpdateCurMana(cost);
        MoveToDiscardPile();
    }

    public bool IsCardPlayable(GameObject TargetedEnemy)
    {
        if(!Player.IsManaSufficent(cost))
            return false;
        if (TargetedEnemy == null)
            foreach (var effect in effects)
                if (effect.TargetTypeFunc == cardGameManager.EffectOnEnemyTargeted)
                    return false;
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
