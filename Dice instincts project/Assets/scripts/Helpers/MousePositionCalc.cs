using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MousePositionCalc
{
    public static Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        float distanceFromCamera = -Camera.main.transform.position.z;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(
            mousePositionScreen.x, mousePositionScreen.y, distanceFromCamera));
        Debug.Log("Mouse Position in World Space (2D): " + mousePositionWorld);
        return mousePositionWorld;
    }

    public static Vector3 GetMousePositionInCanvas(RectTransform canvasRectTransform)
    {
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, mousePositionScreen, null, out mousePositionCanvas);
        Debug.Log("Mouse Position in Canvas: " + mousePositionCanvas);
        return mousePositionCanvas;
    }
}
