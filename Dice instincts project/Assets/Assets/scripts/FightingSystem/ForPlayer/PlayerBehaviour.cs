using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerMana;
    private int curMana;
    private int maxMana = 3;

    public bool IsManaSufficent(int cardCost)
    {
        return curMana >= cardCost;
    }

    public void CurManaToMaxMana()
    {
        curMana = maxMana;
        playerMana.text = curMana.ToString();
    }

    public void UpdateCurMana(int cardCost)
    {
        curMana -= cardCost;
        playerMana.text = curMana.ToString();
    }

    public void UpdateMaxMana(int additionalMaxMana)
    {
        maxMana += additionalMaxMana;
    }
    public override void UpdateHealth(int damage)
    {
        health -= damage;
        CurHealthText.text = health.ToString();
        transform.GetChild(0).GetComponent<HealthBar>().SetHealth(health);
    }

    public void HealHalfHealth() => UpdateHealth(-math.min(startingHealth / 2,startingHealth - health));



    // Start is called before the first frame update
    void Start()
    {
        curMana = maxMana;
        startingHealth = 50;
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
