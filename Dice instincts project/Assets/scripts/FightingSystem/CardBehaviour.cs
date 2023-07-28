using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster CanvasRaycast;
    [SerializeField]
    EventSystem eventSystem;
    public bool hasBeenPlayed;
    public int handIndex;

    private GameManager gm;
    private void OnMouseDown()
    {
        if(hasBeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
            MoveToDiscardPile();
        }
    }
    void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
