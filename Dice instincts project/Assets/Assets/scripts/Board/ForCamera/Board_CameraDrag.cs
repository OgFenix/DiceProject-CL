using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Board_CameraDrag : MonoBehaviour
{
    [SerializeField]
    public bool isInBoardState = false;
    int Zcoord = -10;
    [SerializeField]
    EventSystem eventSystem;
    PointerEventData m_PointerEventData;
    Vector3 startingMousePosition = Vector3.zero;
    Vector3 startingCameraPosition;
    [SerializeField]
    public float zoomSpeed = 2.25f;
    private bool m_Dragging = false;

    private void ZoomCam()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        this.GetComponent<Camera>().orthographicSize = this.GetComponent<Camera>().orthographicSize + scrollWheelInput * zoomSpeed;
    }

    private void OnBeginDrag()
    {
        m_Dragging = true;
        startingMousePosition = m_PointerEventData.position;
        startingCameraPosition = this.transform.position;
    }

    private void Drag()
    {
        Vector3 mousePos = new Vector3(m_PointerEventData.position.x, m_PointerEventData.position.y, -Zcoord);
        this.transform.position = startingCameraPosition + startingMousePosition - mousePos;
    }

    private void OnEndDrag()
    {
        m_Dragging = false;
       startingCameraPosition = this.transform.position;
    }

    private void DragManager()
    {
        m_PointerEventData.position = MousePositionCalc.GetMousePositionInWorldBasedOnOldCameraPosition(startingCameraPosition);
        //Debug.Log($"The position in world is: {MousePositionCalc.GetMousePositionInWorld()}");
        if (Input.GetMouseButtonDown(0))
        {
            OnBeginDrag(); //checking to pickup card
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
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
        startingCameraPosition = this.transform.position;
        m_PointerEventData = new PointerEventData(eventSystem);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInBoardState)
        {
            DragManager();
            if (!m_Dragging)
                ZoomCam();
        }
    }
}
