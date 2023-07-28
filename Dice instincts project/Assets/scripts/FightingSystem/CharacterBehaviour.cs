using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI characterHealth;
    [SerializeField]
    List<Tuple<string, int, int>> enemyIDs = new List<Tuple<string, int, int>>();

    private int health;

    public void UpdateHealth(int damage)
    {
        health -= damage;
    }
    public void startingHealth(int startingHealth)
    {
        health = startingHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Enemy")
            startingHealth(enemyIDs[0].Item2);
        //starting health of every hero is 50
        else
            startingHealth(50);
    }

    // Update is called once per frame
    void Update()
    {
        characterHealth.text = health.ToString();
    }
}
