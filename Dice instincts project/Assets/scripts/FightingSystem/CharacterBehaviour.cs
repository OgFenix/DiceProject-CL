using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CharacterBehaviour : MonoBehaviour
{
    private int health;
    private string characterName;
    public CharacterBehaviour(int health, string characterName)
    {
        this.health = health;
        this.characterName = characterName;
    }

    //get and set functions
    #region
    public string getName()
    {
        return characterName;
    }
    public int getHealth()
    {
        return health;
    }
    public void setName(string characterName)
    {
        this.characterName = characterName;
    }
    public void setHealth(int health)
    {
        this .health = health;
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
