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
    public bool hasBeenPlayed;
    public int handIndex;
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
    public List<Tuple<EffectSelector, int>> effects;


    private void OnMouseDown()
    {
        if(hasBeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            MoveToDiscardPile();
        }
    }

    public void activateCard()
    {
        
    }

    void MoveToDiscardPile()
    { 
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
        cardsDictionary = GameObject.Find("GameDirector").GetComponent<CardsDictionary>();
        thisCard = cardsDictionary.InitializeCard(id);
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

    // Update is called once per frame
    void Update()
    {

    }
}
