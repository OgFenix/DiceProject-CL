using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickCardManager : MonoBehaviour
{

    public enum PickFor{
        upgrade,
        add,
        remove
    }

    [SerializeField]
    private OverallGameManager cardGameManager;
    [SerializeField]
    GraphicRaycaster CanvasRaycast;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    RectTransform canvas;
    [SerializeField]
    DeckScrollMenu deckScrollMenu;
    [SerializeField]
    GameObject scrollContainer;
    [SerializeField]
    GameObject exitDeckMenuBtn;
    public Vector3 handPosition;
    public Vector2 handSize;
    PointerEventData m_PointerEventData;
    private GameObject CardToPick = null;
    public static PickFor pickFor;
    

    private void OnbeginClick()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        /*if (results.Count == 0)
            Debug.Log("Miss");
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        } */
        foreach (RaycastResult result in results)
            if (result.gameObject.tag == "Card" && (result.gameObject.transform.parent.gameObject == this.gameObject || result.gameObject.transform.parent.parent.gameObject == this.gameObject))
            {
                CardToPick = result.gameObject;
                break;
            }
    }

    private void AddCard()
    {
        cardGameManager.AddCardToDeck(CardToPick.GetComponent<CardBehaviour>().id, false);
        DestroyAllCardDiscoverOptions();
    }
    private void UpgradeCard()
    {
        int id = CardToPick.GetComponent<CardBehaviour>().id;
        cardGameManager.RemoveCardFromDeck(CardToPick);
        cardGameManager.AddCardToDeck(id, true);

    }
    private void RemoveCard()
    {

    }

    private void OnEndClick()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
            if (result.gameObject == CardToPick)
            {
                CardToPick = CardToPick.transform.parent.gameObject;
                switch (pickFor)
                {
                    case PickFor.upgrade:
                        UpgradeCard();
                        deckScrollMenu.moveDeckToOverallContainer();
                        scrollContainer.GetComponent<PickCardManager>().enabled = false;
                        exitDeckMenuBtn.gameObject.SetActive(true);
                        break;
                    case PickFor.add:
                        AddCard();
                        break;
                    case PickFor.remove:
                        RemoveCard();
                        break;
                }
                break;
            }
    }

    private void DestroyAllCardDiscoverOptions()
    {
        for (int i = 0;i < transform.childCount;i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    private void DragManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData.position = Input.mousePosition;
            OnbeginClick(); //checking to pickup card
            return;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_PointerEventData.position = Input.mousePosition;
            OnEndClick(); //finish Dragging
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_PointerEventData = new PointerEventData(eventSystem);
    }

    // Update is called once per frame
    void Update()
    {
        DragManager();
    }
}

