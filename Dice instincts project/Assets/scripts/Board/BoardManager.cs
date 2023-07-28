using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    GameObject CombatButton;
    public bool IsInCombat { get; private set; } = false;
    [SerializeField]
    private List<EnemyMovement> enemies = new List<EnemyMovement>();
    GameObject EnemyInCombat = null;

    private void Start()
    {
        CombatButton.SetActive(false);
    }
    public void TurnIsOver()
    {
        foreach(EnemyMovement enemy in enemies)
        {
            enemy.DecideMove();
        }
    }
    public bool IsContainingEnemy(Vector3Int pos)
    {
        foreach (EnemyMovement enemy in enemies)
            if(pos == enemy.EnemyCellPos)
            {
                EnterCombat(enemy);
                return true;
            }
        return false;
    }
    public void EnterCombat(EnemyMovement enemy)
    {
        EnemyInCombat = enemy.gameObject;
        IsInCombat = true;
        CombatButton.SetActive(true);
    }

    private void Update()
    {

    }

    public void CombatButtonPressed()
    {
            if (EnemyInCombat != null)
            {
                enemies.Remove(EnemyInCombat.GetComponent<EnemyMovement>());
                GameObject.Destroy(EnemyInCombat);
            }
            CombatButton.SetActive(false);
            IsInCombat = false;
    }    
}
