using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{

    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> discardPile = new List<GameObject>();
    [SerializeField]
    TextMeshProUGUI deckAmount;

    

    public void DrawCard()
    {
        GameObject randCard = deck[Random.Range(0, deck.Count)];
        CardBehaviour curCard = Instantiate(randCard).GetComponent<CardBehaviour>();
        curCard.transform.SetParent(transform);
        

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
}
