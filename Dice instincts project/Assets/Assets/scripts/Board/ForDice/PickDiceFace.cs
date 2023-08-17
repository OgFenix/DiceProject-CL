using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickDiceFace : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1250f;
    [SerializeField]
    private Camera canvasCam;
    private Vector3 _initialMousePosition;
    private Quaternion _initialRotation;
    [SerializeField]
    private bool _isindragmode = true;
    private Dictionary<int,int> _translateToRepDiceFace = new Dictionary<int, int>() {{-3, 1},{-2, 4},{-1, 5},{1,2},{2,3},{3,6}};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isindragmode)
            DragManager();
        else
            PickDiceFaceManager();
    }

    private void PickDiceFaceManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = canvasCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 localHitPosition = hitInfo.transform.InverseTransformPoint(hitInfo.point);
                FindChosenDiceFaceIndex(localHitPosition);
                Debug.Log("Mouse Click Position: " + localHitPosition);
            }
        }
    }

    private void FindChosenDiceFaceIndex(Vector3 localHitPosition)
    {
        float[] cordinates = new float[3] { localHitPosition.x, localHitPosition.y, localHitPosition.z };
        float[] absCordinates = new float[3] { Math.Abs(localHitPosition.x), Math.Abs(localHitPosition.y), Math.Abs(localHitPosition.z) };
        int Ind = absCordinates.ToList().IndexOf(absCordinates.Max());
        Ind++;
        if (cordinates[Ind - 1] < 0)
            Ind = -Ind;
        int ChosenDiceFace = _translateToRepDiceFace[Ind];
        Debug.Log(ChosenDiceFace);
    }

    private void DragManager()
    {
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
            //OnEndDrag(); //finish Dragging
        }
    }

    private void OnBeginDrag()
    {
        _initialMousePosition = Input.mousePosition;
        _initialRotation = transform.GetChild(0).rotation;
        Debug.Log($"Initial Rotation: {_initialRotation}");
    }
    private void Drag()
    {
        Vector3 mouseDelta = Input.mousePosition - _initialMousePosition;
        Debug.Log($"X: {mouseDelta.x} Y: {mouseDelta.y}");

        Quaternion rotationDeltaX = Quaternion.Euler(Vector3.up * mouseDelta.x * rotationSpeed);
        Quaternion rotationDeltaY = Quaternion.Euler(Vector3.left * mouseDelta.y * rotationSpeed);
        Quaternion newRotation = _initialRotation * rotationDeltaX * rotationDeltaY;

        Debug.Log($"New Rotation: {newRotation.eulerAngles}");
        transform.GetChild(0).rotation = newRotation;
    } 
    private void OnEndDrag()
    {
        throw new NotImplementedException();
    }
}
