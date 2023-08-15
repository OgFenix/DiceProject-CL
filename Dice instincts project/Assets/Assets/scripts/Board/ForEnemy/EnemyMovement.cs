using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    OverallGameManager overallGameManager;
    private EnemyDictionary enemyDictionary;
    public int EnemyID;
    Tilemap tilemap;
    PlayerMovement Player;
    List<Vector3Int> MoveableDirections = new List<Vector3Int>()
    {
        new Vector3Int(1, 0),new Vector3Int(0, 1),new Vector3Int(0, -1),new Vector3Int(-1, 0)
    };
    public Vector3Int EnemyCellPos { get; private set; }
    int PrevMoveInd;
    // Start is called before the first frame update
    void Start()
    {
        SetEnemyID();
        overallGameManager = GameObject.Find("GameDirector").GetComponent<OverallGameManager>();
        Player = GameObject.Find("PlayerInBoard").GetComponent<PlayerMovement>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        PrevMoveInd = -1;
        EnemyCellPos = tilemap.WorldToCell(gameObject.transform.position);
    }

    private void SetEnemyID()
    {
        enemyDictionary = GameObject.Find("GameDirector").GetComponent<EnemyDictionary>();
        EnemyID = enemyDictionary.GetRandomID();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DecideMove()
    {
        List<int> AvailableMoves = GetAvailableMovementsInds();
        float[] PrecsOfAvailableMoves = new float[AvailableMoves.Count];
        for (int i = 0; i < PrecsOfAvailableMoves.Length; i++)
            PrecsOfAvailableMoves[i] = 1f / (float)(AvailableMoves.Count);
        if (AvailableMoves.Contains(PrevMoveInd) && AvailableMoves.Count > 1)
        {
            if (Random.Range(0, 4) == 4)
            {
                EnemyCellPos = MoveableDirections[AvailableMoves[PrevMoveInd]] + EnemyCellPos;
                gameObject.transform.position = tilemap.GetCellCenterWorld(EnemyCellPos);
                return;
            }
            AvailableMoves.Remove(PrevMoveInd);
            /*PrecsOfAvailableMoves[PrevMoveInd] = PrecsOfAvailableMoves[PrevMoveInd] / AvailableMoves.Count;
            for (int i = 0; i < PrecsOfAvailableMoves.Length; i++)
                if (i != PrevMoveInd)
                    PrecsOfAvailableMoves[i] = PrecsOfAvailableMoves[i] + PrecsOfAvailableMoves[PrevMoveInd] / AvailableMoves.Count;
            for (int i = 1; i < PrecsOfAvailableMoves.Length; i++)
                PrecsOfAvailableMoves[i] = PrecsOfAvailableMoves[i] + PrecsOfAvailableMoves[i - 1];
            for (int i = 0;i < PrecsOfAvailableMoves.Length;i++)
                PrecsOfAvailableMoves[0] = PrecsOfAvailableMoves[0] * math.pow(AvailableMoves.Count, 2); */

        }
        //Debug.Log($"The Rand: {Random.Range(0, AvailableMoves.Count)}");
        EnemyCellPos = MoveableDirections[AvailableMoves[Random.Range(0,AvailableMoves.Count)]] + EnemyCellPos;
        gameObject.transform.position = tilemap.GetCellCenterWorld(EnemyCellPos);
    }

    private List<int> GetAvailableMovementsInds()
    {
        var AvailableMovementsInds = new List<int>();
        for (int i = 0; i < MoveableDirections.Count; i++)
        {
            if(IsDirectionViable(MoveableDirections[i]))
                AvailableMovementsInds.Add(i);
        }
        return AvailableMovementsInds;
    }

    private bool IsDirectionViable(Vector3Int direction)
    {
        TileBase tile = tilemap.GetTile(direction + EnemyCellPos);
        if (tile != null)
            return true;
        return false;
    }
}
