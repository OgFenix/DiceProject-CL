using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverallGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    TextMeshProUGUI deckAmount;
    [SerializeField]
    private GameObject cardContainer;
    List<int> startingDeckIDs = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 1, 2 };
    public List<GameObject> deck { get; private set; }
    private void initializeDeck(List<GameObject> deck)
    {
        GameObject curCard;
        foreach (var cardID in startingDeckIDs)
        {
            curCard = Instantiate(cardPrefab);
            curCard.GetComponent<CardBehaviour>().CreateCard(cardID);
            curCard.SetActive(false);
            deck.Add(curCard);
        }
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
        
    }
}
