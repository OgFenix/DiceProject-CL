using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{

    [SerializeField]
    private TextMeshProUGUI playerCurMana;
    [SerializeField]
    private TextMeshProUGUI playerMaxMana;
    

    private int curMana;
    private int maxMana;
    


    public void UpdateCurMana(int cardCost)
    {
        curMana -= cardCost;
    }

    public void UpdateMaxMana(int additionalMaxMana)
    {
        maxMana += additionalMaxMana;
    }

   

    // Start is called before the first frame update
    void Start()
    {
        maxMana = 3;
        curMana = 3;
    }

    // Update is called once per frame
    void Update()
    {
        playerCurMana.text = curMana.ToString();
        playerMaxMana.text = maxMana.ToString();
        
    }
}
