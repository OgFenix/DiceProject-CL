using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class ExpandChildrenToCell
{

    [SerializeField]
    static float x = 1.5f;
    [SerializeField]
    static float y = 1.5f;
    public static void SetChildrenToCellSize(GameObject container)
    {
        int childrenAmount = container.transform.childCount;
        for (int i = 0; i < childrenAmount; i++)
        {
            Vector3 newScale = new Vector3(x, y, 0);
            container.transform.GetChild(i).transform.localScale = newScale;
        }
    }
}
