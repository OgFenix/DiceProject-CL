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
    [SerializeField]
    private CardsDictionary cardsDictionary;

    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI manacostText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;

    public int id;
    public string name;
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
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GetChildrenComponents();
        cardsDictionary = GameObject.Find("GameDirector").GetComponent<CardsDictionary>();
        int cardID = UnityEngine.Random.Range(0, 3); // make it take from ids in deck later
        thisCard = cardsDictionary.InitializeCard(cardID);
        //creating card from thiscard
        id = thisCard.id;
        name = thisCard.name;
        cost = thisCard.cost;
        cardDisc = thisCard.cardDisc;
        cardSprite = thisCard.cardImage;
        cardForClass = thisCard.cardForClass;
        effects = thisCard.effects;
        cardImage.sprite = cardSprite;
        cardNameText.text = name;
        manacostText.text = cost.ToString();
        descriptionText.text = cardDisc;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
