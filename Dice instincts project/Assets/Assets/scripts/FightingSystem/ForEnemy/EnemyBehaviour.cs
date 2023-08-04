using System;
using System.Collections;
using System.Collections.Generic;
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

    public int id = -1;
    public int attack;
    public List<FuncArgs> enemiesEffectsList;



    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemyInit)
        {
            id = 0;
            CreateEnemy(id);
        }
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
                default: break;
            }
        }
    }
    private FuncArgs getEnemyEffect(List<FuncArgs> enemiesEffectsList)
    {
        FuncArgs res;
        res = enemiesEffectsList[UnityEngine.Random.Range(0, enemiesEffectsList.Count - 1)];
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
        thisEnemy = (Enemy)enemyDictionary.InitializeByID(id);
        characterName = thisEnemy.enemyName;
        attack = thisEnemy.attack;
        startingHealth = thisEnemy.health;
        health = startingHealth;
        SetArmor(0);
        enemiesEffectsList = thisEnemy.enemiesEffectsList;
        enemyImage.sprite = thisEnemy.enemyImage;
        CurHealthText.text = health.ToString();
        FuncArgs firstEffect = getEnemyEffect(enemiesEffectsList);
        enemyActionCurText.text = ChooseEnemyAttackType(firstEffect.FuncToRun) + ", " + firstEffect.EffectNum.ToString();
        isEnemyInit = true;
    }
    public void EnemyAttack()
    {
        foreach (var effect in enemiesEffectsList)
            if (effect.Timing == EffectTiming.Immidiate)
                overallGameManager.ImmidateActivate(this.gameObject, effect);
            else
                overallGameManager.SubscribeToReleventEvent(effect.Timing, ActivateEffect);
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
                effect.TargetTypeFunc(this, effect);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

   
}
