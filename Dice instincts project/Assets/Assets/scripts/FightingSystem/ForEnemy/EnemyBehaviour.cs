using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : CharacterBehaviour
{
    private CardGameManager cardGameManager;
    public OverallGameManager overallGameManager;
    private EnemyDictionary enemyDictionary;
    private Enemy thisEnemy;
    private bool isEnemyInit = false;
    public TextMeshProUGUI enemyActionCurText;
    public Image enemyImage;
    private FuncArgs CurEffect;
    public int id = -1;
    public List<FuncArgs> enemiesEffectsList;



    // Start is called before the first frame update
    void Start()
    {

    }


    private void GetChildrenComponents()
    {
        
        for (int i = 0; i<transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            GameObject child = childTransform.gameObject;
            switch (child.tag)
            {
                case "EnemyImage":
                    enemyImage = child.GetComponent<Image>();
                    break;
                case "EnemyCurHealth":
                    CurHealthText = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "EnemyCurBlock":
                    CurBlockText = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "EnemyCurEffect":
                    enemyActionCurText = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "StatusContainer":
                    statusContainer = child.GetComponent<GameObject>();
                    break;
                default: break;
            }
        }
    }
    private FuncArgs getAndDeclareEnemyNextEffect(List<FuncArgs> enemiesEffectsList)
    {
        FuncArgs res;
        res = enemiesEffectsList[UnityEngine.Random.Range(0, enemiesEffectsList.Count)];
        enemyActionCurText.text = ChooseEnemyAttackType(res.FuncToRun) + ", " + res.EffectNum.ToString();
        return res;
    }

    public void CreateEnemy(int id)
    {
        GetChildrenComponents();
        GameObject gamedirector = GameObject.Find("GameDirector");
        overallGameManager = gamedirector.GetComponent<OverallGameManager>();
        cardGameManager = gamedirector.GetComponent<CardGameManager>();
        cardGameManager.activeenemies.Add(this);
        enemyDictionary = GameObject.Find("GameDirector").GetComponent<EnemyDictionary>();
        statusPrefab = GameObject.Find("PlayerStats").GetComponent<PlayerBehaviour>().statusPrefab;
        thisEnemy = (Enemy)enemyDictionary.InitializeByID(id);
        characterName = thisEnemy.enemyName;
        startingHealth = thisEnemy.health;
        health = startingHealth;
        SetArmor(0);
        enemiesEffectsList = thisEnemy.enemiesEffectsList;
        enemyImage.sprite = thisEnemy.enemyImage;
        CurHealthText.text = health.ToString();
        CurEffect = getAndDeclareEnemyNextEffect(enemiesEffectsList);
        isEnemyInit = true;
    }
    public void EnemyAttack()
    {
        if (this == null)
            return; //if gameobject no longer exists
        if (CurEffect.Timing == EffectTiming.Immidiate)
            overallGameManager.ActivateEffect(this.gameObject, CurEffect);
        else
            overallGameManager.SubscribeToReleventEvent(CurEffect.Timing, ActivateEffect);
        CurEffect = getAndDeclareEnemyNextEffect(enemiesEffectsList);
    }
    private string ChooseEnemyAttackType(EventHandler<FuncArgs> ActionToDo)
    {
        if (ActionToDo == cardGameManager.DealDamage)
            return "Attack";
        if (ActionToDo == cardGameManager.GainBlock)
            return "Defend";
        return string.Empty;
    }
    void ActivateEffect(EffectTiming Timing)
    {
        foreach (var effect in enemiesEffectsList)
            if (effect.Timing == Timing)
                overallGameManager.ActivateEffect(this, effect);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

   
}
