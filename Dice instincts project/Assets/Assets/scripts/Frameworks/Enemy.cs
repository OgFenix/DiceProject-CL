using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FrameworkOfObject
{
    public override int id { get; protected set; }
    public string enemyName;
    public int health;
    public Sprite enemyImage;
    public int attack;
    public List<Tuple<EnemiesEffectSelector, int>> enemiesEffectsList;
    public Enemy(int id, string enemyName,  int health, Sprite enemyImage, int attack, List<Tuple<EnemiesEffectSelector, int>> enemiesEffectsList)
    {
        this.id = id;
        this.enemyName = enemyName;
        this.health = health;
        this.enemyImage = enemyImage;
        this.attack = attack;
        this.enemiesEffectsList = enemiesEffectsList;
    }
}
