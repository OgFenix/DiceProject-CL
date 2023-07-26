using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBehaviour
{
    private int attack;
    public EnemyBehaviour(int health, string characterName, int attack) : base(health, characterName)
    {
        this.attack = attack;
    }

    // get and set function
    #region
    public int getAttack()
    {
        return attack;
    }
    public void setAttack(int attack)
    {
        this.attack = attack;
    }
    #endregion 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
