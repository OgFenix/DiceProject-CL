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
    private List<GameObject> deck = new List<GameObject>();
    private List<GameObject> discardPile = new List<GameObject>();
    [SerializeField]
    TextMeshProUGUI deckAmount;
    

    public void DrawCard()
    {
        GameObject randCard = deck[Random.Range(0, deck.Count)];
        CardBehaviour curCard = Instantiate(randCard).GetComponent<CardBehaviour>();
        curCard.transform.SetParent(transform);
        

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
