using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardDragManager : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster CanvasRaycast;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    RectTransform canvas;
    public Vector3 handPosition;
    public Vector2 handSize;
    PointerEventData m_PointerEventData;
    private GameObject CardToDrag = null;
    private Vector3 StartingPos;

    private void OnBeginDrag()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        /*if (results.Count == 0)
            Debug.Log("Miss");
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        } */
        if (results.Count == 0)
            return;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "Card" && (result.gameObject.transform.parent.gameObject == this.gameObject || result.gameObject.transform.parent.parent.gameObject == this.gameObject))
            {
                StartingPos = result.gameObject.transform.position;
                if (result.gameObject.transform.parent.tag == "Card")
                    CardToDrag = result.gameObject.transform.parent.gameObject;
                else
                    CardToDrag = result.gameObject;
                CardToDrag.transform.position = m_PointerEventData.position;
                break;
                
            }
        }
    }

    private void Drag()
    {
        if (CardToDrag != null) 
            CardToDrag.transform.position = m_PointerEventData.position;
    }
    public bool IsMouseInHand()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
            if (result.gameObject.tag == "Hand")
            {
                return true;
            }
        return false;
    }

    private void OnEndDrag()
    {
        if (CardToDrag == null)
            return;
        Vector3 mousePos = MousePositionCalc.GetMousePositionInCanvas(canvas);
        if (IsMouseInHand())
        {
            CardToDrag.transform.position = StartingPos;
            CardToDrag = null;
            return;
        }
        GameObject Enemytargeted = null;
        List<RaycastResult> results = new List<RaycastResult>();
        CanvasRaycast.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
            if (result.gameObject.tag == "EnemyImage")
            {
                Enemytargeted = result.gameObject.transform.parent.gameObject;
                break;
            }
        if (CardToDrag.GetComponent<CardBehaviour>().IsCardPlayable(Enemytargeted))
            CardToDrag.GetComponent<CardBehaviour>().PlayCard(Enemytargeted);
        else
            CardToDrag.transform.position = StartingPos;
        CardToDrag = null;

    }

    private void DragManager()
    {
        m_PointerEventData.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) {
            OnBeginDrag(); //checking to pickup card
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            Drag(); //dragging card
            return;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Ending Drag");
            OnEndDrag(); //finish Dragging
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
