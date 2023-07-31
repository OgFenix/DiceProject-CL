using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListRandomizer 
{
    public static List<GameObject> Randomize<GameObject>(this List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
}
