using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : CharacterBehaviour
{
    private EnemyDictionary enemyDictionary;
    private Enemy thisEnemy;
    private bool isEnemyInit = false;

    public TextMeshProUGUI enemyHealthCurText;
    public TextMeshProUGUI enemyActionCurText;
    public Image enemyImage;

    public int id = -1;
    public int attack;
    public List<Tuple<EnemiesEffectSelector, int>> enemiesEffectsList;



    // Start is called before the first frame update
    void Start()
    {
        if (!isEnemyInit)
        {
            id = 0;
            CreateEnemy(id);
        }
    }
    public override void UpdateHealth(int damage)
    {
        health -= damage;
    }

    public override void startingHealth(int startingHealth)
    {
        health = startingHealth;
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
                    enemyHealthCurText = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "EnemyCurEffect":
                    enemyActionCurText = child.GetComponent<TextMeshProUGUI>();
                    break;
                default: break;
            }
        }
    }
    private Tuple<EnemiesEffectSelector, int> getEnemyEffect(List<Tuple<EnemiesEffectSelector, int>> enemiesEffectsList)
    {
        Tuple<EnemiesEffectSelector, int> res;
        res = enemiesEffectsList[UnityEngine.Random.Range(0, enemiesEffectsList.Count - 1)];
        return res;
    }
    public void CreateEnemy(int id)
    {
        GetChildrenComponents();
        enemyDictionary = GameObject.Find("GameDirector").GetComponent<EnemyDictionary>();
        thisEnemy = (Enemy)enemyDictionary.InitializeByID(id);
        characterName = thisEnemy.enemyName;
        attack = thisEnemy.attack;
        health = thisEnemy.health;
        enemiesEffectsList = thisEnemy.enemiesEffectsList;
        enemyImage.sprite = thisEnemy.enemyImage;
        enemyHealthCurText.text = health.ToString();
        Tuple<EnemiesEffectSelector, int> firstEffect = getEnemyEffect(thisEnemy.enemiesEffectsList);
        enemyActionCurText.text = firstEffect.Item1.ToString() + ", " + firstEffect.Item2.ToString();
        isEnemyInit = true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

   
}
