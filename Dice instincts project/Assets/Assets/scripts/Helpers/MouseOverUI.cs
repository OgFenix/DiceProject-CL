using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MouseOverUI
{
    public static bool IsMouseOverGameObject(GameObject GameObjectToCheck)
    {
        // Perform the raycast using EventSystem
        List<RaycastResult> results = GetAllUIGameObjectOverMouse();

        // Check if the targeted UI object is among the raycast results
        foreach (RaycastResult result in results)
        {
            //Debug.Log($"Im over {result.gameObject.name}");
            if (result.gameObject == GameObjectToCheck)
                return true;
        }

        return false;
    }

    public static List<RaycastResult> GetAllUIGameObjectOverMouse()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Perform the raycast using EventSystem
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }
}
