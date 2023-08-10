using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleToCanvasSize : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;
    private Vector2 lastScreenSize;
    // Start is called before the first frame update
    private void Start()
    {
        lastScreenSize = new Vector2(_canvas.GetComponent<RectTransform>().rect.width, _canvas.GetComponent<RectTransform>().rect.height);
        ChangeGameObjectsDimensions();
    }

    private void Update()
    {
        if (_canvas.GetComponent<RectTransform>().rect.width != lastScreenSize.x || _canvas.GetComponent<RectTransform>().rect.height != lastScreenSize.y)
        {
            ChangeGameObjectsDimensions();
            lastScreenSize.x = _canvas.GetComponent<RectTransform>().rect.width;
            lastScreenSize.y = _canvas.GetComponent<RectTransform>().rect.width;
        }
    }

    private void ChangeGameObjectsDimensions()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(_canvas.GetComponent<RectTransform>().rect.width, _canvas.GetComponent<RectTransform>().rect.height);
    }
}
