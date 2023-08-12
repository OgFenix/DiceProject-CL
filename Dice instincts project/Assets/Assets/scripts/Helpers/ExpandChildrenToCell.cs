using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class ExpandChildrenToCell
{
    public static void SetChildrenToCellSize(GameObject container, float x, float y)
    {
        int childrenAmount = container.transform.childCount;
        for (int i = 0; i < childrenAmount; i++)
        {
            Vector3 newScale = new Vector3(x, y, 0);
            container.transform.GetChild(i).transform.localScale = newScale;
        }
    }

    public static void SetGameObjectToNewSize(GameObject NewChild, float x, float y)
    {
        Vector3 newScale = new Vector3(x, y, 0);
        NewChild.transform.localScale = newScale;
    }
}
