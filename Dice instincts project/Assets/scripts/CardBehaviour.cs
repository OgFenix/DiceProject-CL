using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private int cost;
    private string type;
    private string cardName;
    public HandZone handZode;
    public CardBehaviour(int cost, string type, string cardName)
    {
        this.cost = cost;
        this.type = type;
        this.cardName = cardName;
    }

    //get and set functions
    #region
    public int getCost()
    {
        return cost;
    }
    public void setCost(int cost)
    {
        this.cost = cost;
    }
    public void setType(string type)
    {
        this.type = type;
    }
    public string getType()
    {
       return type;
    }
    public void setCardName(string cardName)
    {
        this.cardName = cardName;
    }
    public string getCardName()
    {
        return cardName;
    }
    #endregion 

    public void OnClick()
    {
        handZode.AddCardToHand(this);
        Debug.Log("Card Clicked!");
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
