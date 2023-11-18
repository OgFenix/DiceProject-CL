using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class PlayerBehaviour : CharacterBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerMana;
    private int curMana;
    private int maxMana = 3;
    public Sprite fullManaSprite;
    public Sprite emptyManaSprite;
    public Image[] manaUnits;
    [SerializeField]
    public GameObject manaBar;
    [SerializeField]
    Animator manaToFull;



    public bool IsManaSufficent(int cardCost)
    {
        return curMana >= cardCost;
    }

    public void CurManaToMaxMana()
    {
        curMana = maxMana;
        playerMana.text = curMana.ToString();
        UpdateManaBar();
    }

    public void UpdateCurMana(int cardCost)
    {
        curMana -= cardCost;
        playerMana.text = curMana.ToString();
        disableMana();
        UpdateManaBar();


    }
    private void UpdateManaBar()
    {
        for (int i = 0; i < curMana; i++)
        {
  
                Debug.Log(manaUnits[i].gameObject);
            // Set the full mana image for the filled units.
            manaUnits[i].GetComponent<Image>().sprite = fullManaSprite;
            //StartCoroutine(manaAnimation()); 

        }
       
    }

    private void disableMana()
    {
        for (int i = 0; i < maxMana; i++)
        {
            manaUnits[i].GetComponent<Image>().sprite = emptyManaSprite;
        }
    }
    private void startUpdateManaBar()
    {
        for (int i = 0; i < maxMana; i++)
        {
                manaUnits[i].gameObject.SetActive(true);
        }
    }
    private IEnumerator manaAnimation()
    {
        while (true)
            yield return null;
        manaToFull.Play("manaAnimationRealFr", 0, 0.0f);
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
        //manaUnits = GetComponentsInChildren<Image>();
        //startUpdateManaBar();


    }

    // Update is called once per frame
    void Update()
    {
    }
}
