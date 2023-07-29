using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickCardManager : MonoBehaviour
{
    [SerializeField]
    private CardGameManager cardGameManager;
    [SerializeField]
    GraphicRaycaster CanvasRaycast;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    RectTransform canvas;
    public Vector3 handPosition;
    public Vector2 handSize;
    PointerEventData m_PointerEventData;
    private GameObject CardToPick = null;

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

    private void OnEndClick()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
            if (result.gameObject == CardToPick)
            {
                CardToPick = CardToPick.transform.parent.gameObject;
                cardGameManager.AddCardToDeck(CardToPick.GetComponent<CardBehaviour>().id);
                DestroyAllCardDiscoverOptions();
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

