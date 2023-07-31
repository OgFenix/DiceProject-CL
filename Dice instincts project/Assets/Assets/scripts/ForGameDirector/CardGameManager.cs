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
    TextMeshProUGUI deckAmount;

    [SerializeField]
    private List<GameObject> deck = new List<GameObject>();

    [SerializeField]
    private GameObject hand;

    private List<GameObject> discardPile = new List<GameObject>();
    OverallGameManager gameManager;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deckAmount.text = deck.Count.ToString();
    }

    public void AAAAA(object sender, FuncArgs Args)
    {

    }
}
