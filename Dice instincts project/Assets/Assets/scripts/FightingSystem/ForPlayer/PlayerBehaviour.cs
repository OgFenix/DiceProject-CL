using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{

    [SerializeField]
    private TextMeshProUGUI playerMana;
    [SerializeField]
    private TextMeshProUGUI playerHealth;


    private int maxHealth;
    private int curMana;
    private int maxMana;

    public override void startingHealth(int startingHealth)
    {
        health = startingHealth;
    }

    public void UpdateCurMana(int cardCost)
    {
        curMana -= cardCost;
    }

    public override void UpdateHealth(int damage)
    {
        health -= damage;
    }

    public void UpdateMaxMana(int additionalMaxMana)
    {
        maxMana += additionalMaxMana;
    }

   

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 50;
        startingHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
