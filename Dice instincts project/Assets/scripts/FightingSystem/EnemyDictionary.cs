using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDictionary : MonoBehaviour
{
    //     Name - Health - Attack
    List<Tuple<string, int, int>> enemyIDs = new List<Tuple<string, int, int>>();

    private void Start()
    {
        enemyIDs.Add(new Tuple<string, int, int>("Mondo", 15, 5));
    }

    private void Update()
    {
        
    }

}
