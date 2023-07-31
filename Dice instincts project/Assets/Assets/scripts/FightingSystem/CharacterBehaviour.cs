using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

abstract public class CharacterBehaviour : MonoBehaviour
{


    public List<CharacterStatus> statusesList;

    public virtual string characterName { get; set; }
    public virtual int health { get; set; }
    public virtual int block { get; set; }

    public abstract void UpdateHealth(int damage);
    public abstract void startingHealth(int startingHealth);

    // Start is called before the first frame update
    void Start()
    {
        startingHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
