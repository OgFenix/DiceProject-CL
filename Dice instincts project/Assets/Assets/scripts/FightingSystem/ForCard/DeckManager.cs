using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    GameObject hand;
    [SerializeField]
    TextMeshProUGUI curDeckSize;
    List<GameObject> deck = new List<GameObject>();
    CardsDictionary cards = new CardsDictionary();
    

    

    private void DrawCard()
    {
     
    }
    private void Start()
    {
        
    }
}
