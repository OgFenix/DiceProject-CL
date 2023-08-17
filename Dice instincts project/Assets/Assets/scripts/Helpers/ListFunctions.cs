using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListFunctions<T> 
{
    public static List<T> Randomize(List<T> list)
    {
        if (list == null)
            return list;
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
    public static List<GameObject> SortListByName(List<GameObject> list)
    {
        list.Sort((obj1, obj2) => obj1.name.CompareTo(obj2.name));
        return list;
    }
    public static void SortChildren(GameObject parentObject)
    {
        int childCount = parentObject.transform.childCount;
        List<Transform> childList = new List<Transform>();

        // Add all children to the list
        for (int i = 0; i < childCount; i++)
        {
            childList.Add(parentObject.transform.GetChild(i));
        }

        // Sort the list alphabetically based on child names
        childList.Sort((a, b) => string.Compare(a.name, b.name));

        // Rearrange the children based on the sorted list
        for (int i = 0; i < childCount; i++)
        {
            childList[i].SetSiblingIndex(i);
        }
    }
}
