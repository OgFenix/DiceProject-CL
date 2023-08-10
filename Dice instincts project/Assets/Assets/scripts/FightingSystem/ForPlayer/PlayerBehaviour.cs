using System.Collections;
using System.Collections.Generic;
using TMPro;
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

   

    // Start is called before the first frame update
    void Start()
    {
        CurHealthText = this.transform.Find("HealthPlayer").GetComponent<TextMeshProUGUI>();
        CurBlockText = this.transform.Find("Armor").GetComponent<TextMeshProUGUI>();
        curMana = maxMana;
        startingHealth = 50;
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
