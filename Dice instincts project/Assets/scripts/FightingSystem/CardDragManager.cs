using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragManager : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster CanvasRaycast;
    [SerializeField]
    EventSystem eventSystem;
    PointerEventData m_PointerEventData;
    private GameObject CardToDrag = null;

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
            if (result.gameObject.tag == "Card")
            {
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

    private void OnEndDrag()
    {
        CardToDrag = null;
    }

    private void DragManager()
    {
        //m_PointerEventData.position = Input.mousePosition;
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
