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
    GameObject CombatButtonPrefab;
    GameObject CombatButton = null;
    public bool IsInCombat { get; private set; } = false;
    [SerializeField]
    private List<EnemyMovement> enemies = new List<EnemyMovement>();
    GameObject EnemyInCombat = null;
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
                EnemyInCombat = enemy.gameObject;
                EnterCombat();
                return true;
            }
        return false;
    }
    private void EnterCombat()
    {
        IsInCombat = true;
        CombatButton = GameObject.Instantiate(CombatButtonPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsCombatButtonPressed();
        }
    }

    private void IsCombatButtonPressed()
    {
        Vector3 MousePos = MousePositionCalc.GetMousePositionInWorld();
        if(CombatButton != null && CombatButton.GetComponent<BoxCollider2D>().OverlapPoint(new Vector2(MousePos.x, MousePos.y)))
        {
            if (EnemyInCombat != null)
            {
                enemies.Remove(EnemyInCombat.GetComponent<EnemyMovement>());
                GameObject.Destroy(EnemyInCombat);
            }
            GameObject.Destroy(CombatButton);
            CombatButton = null;
            IsInCombat = false;
        }
    }    
}
