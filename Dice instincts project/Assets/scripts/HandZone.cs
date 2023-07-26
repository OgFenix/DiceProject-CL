using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandZone : MonoBehaviour
{
    public GameObject cardPrefab;
    public int maxCardsInHand = 10;
    private float cardSpacing = 1.0f;
    private Vector2 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }
    public void AddCardToHand(CardBehaviour card)
    {
        if(transform.childCount >= maxCardsInHand)
        {
            Debug.Log("Hand is full!");
            return;
        }

        card.transform.SetParent(transform);

        int cardIndex = transform.childCount - 1;
        Vector2 newPosition = initialPosition + new Vector2(cardSpacing * cardIndex, 0f);
        card.transform.position = newPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
