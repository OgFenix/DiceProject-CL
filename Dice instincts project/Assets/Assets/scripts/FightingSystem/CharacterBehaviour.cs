using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

abstract public class CharacterBehaviour : MonoBehaviour
{


    public List<CharacterStatus> statusesList;

    public string characterName;
    public int startingHealth;
    public int health;
    public int block;
    protected TextMeshProUGUI CurHealthText;
    protected TextMeshProUGUI CurBlockText;

    public void UpdateHealth(int damage)
    {
        health -= damage;
        CurHealthText.text = health.ToString();
    }
    public void ChangeArmor(int num) //give negative number for reduction and positive to increase
    {
        block += num;
        CurBlockText.text = block.ToString();
    }
    public void SetArmor(int num)
    {
        block = num;
        CurBlockText.text = block.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
