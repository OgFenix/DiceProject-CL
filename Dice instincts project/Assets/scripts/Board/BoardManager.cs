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
    GameObject PossibleEndingSquaresList;
    [SerializeField]
    DiceBehaviour Dice;
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject PossibleEndingSquresPrefab;
    Vector3Int cellPlayerPosition;
    List<Vector3Int> MoveableDirections = new List<Vector3Int>()
    {
        new Vector3Int(1, 0),new Vector3Int(0, 1),new Vector3Int(0, -1),new Vector3Int(-1, 0)
    };
    List<Vector3Int> PossibleEndingSqures = new List<Vector3Int>();
    List<Vector3> PossibleEndingSquresPos = new List<Vector3>();
    List<GameObject> PossibleEndingSquaresMarks = new List<GameObject>();

    void Start()
    {
        cellPlayerPosition = tilemap.WorldToCell(Player.transform.position);
    }

    public void initializePossibleEndingSqures(int ToMove)
    {
        PossibleEndingSqures.Clear();
        PossibleEndingSquresPos.Clear();
        GetAllPossibleEndingSqures(ToMove, cellPlayerPosition, new List<int>());
        foreach (var PossibleEndingSqure in PossibleEndingSqures)
            PossibleEndingSquresPos.Add(tilemap.GetCellCenterWorld(PossibleEndingSqure));
        MarkPossibleEndingSqures();
    }

    private void GetAllPossibleEndingSqures(int ToMove, Vector3Int PosToCheck,List<int> NonViableDirInds)
    {
        bool PathFound = false;
        if(ToMove == 0)
        { 
            PossibleEndingSqures.Add(PosToCheck);
            return;
        }
        List<int> NewNonViableDirInds = new List<int>();
        for (int i = 0; i < MoveableDirections.Count; i++)
        {
            if (NonViableDirInds.Contains(i))
                continue;
            PosToCheck = PosToCheck + MoveableDirections[i];
            TileBase tile = tilemap.GetTile(PosToCheck);
            if (tile != null)
            {
                PathFound = true;
                NewNonViableDirInds.Add(3 -  i); // 3-i is the by design the opposite direction
                GetAllPossibleEndingSqures(ToMove - 1, PosToCheck, NewNonViableDirInds);
                NewNonViableDirInds.Clear();
            }
            PosToCheck = PosToCheck - MoveableDirections[i];
        }
        if (!PathFound)
        {
            foreach(int i in NonViableDirInds)
            {
                PosToCheck = PosToCheck + MoveableDirections[i];
                TileBase tile = tilemap.GetTile(PosToCheck);
                if (tile != null)
                {
                    PathFound = true;
                    NewNonViableDirInds.Add(3 - i); // 3-i is the by design the opposite direction
                    GetAllPossibleEndingSqures(ToMove - 1, PosToCheck, NewNonViableDirInds);
                    NewNonViableDirInds.Clear();
                }
                PosToCheck = PosToCheck - MoveableDirections[i];
            }
        }
    }

    private void MarkPossibleEndingSqures()
    {
        foreach (var PossibleEndingSqurePos in PossibleEndingSquresPos)
        { 
            GameObject NewPossibleEndingSqure = Instantiate(PossibleEndingSquresPrefab);
            NewPossibleEndingSqure.transform.position = PossibleEndingSqurePos;
            PossibleEndingSquaresMarks.Add(NewPossibleEndingSqure);
            NewPossibleEndingSqure.transform.SetParent(PossibleEndingSquaresList.transform);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = MousePositionCalc.GetMousePositionInWorld();
            foreach (var PossibleEndingSquareMark in PossibleEndingSquaresMarks)
            {
                if (PossibleEndingSquareMark.GetComponent<BoxCollider2D>().OverlapPoint(new Vector2(MousePos.x, MousePos.y)))
                {
                    Dice.IsRollAllowed = true;
                    Player.transform.position = PossibleEndingSquareMark.transform.position;
                    cellPlayerPosition = tilemap.WorldToCell(Player.transform.position);
                    foreach (var PossibleEndingSquareMark1 in PossibleEndingSquaresMarks)
                    {
                        GameObject.Destroy(PossibleEndingSquareMark1.gameObject);
                    }
                    PossibleEndingSquaresMarks.Clear();
                    break;
                }
            }
        }
    }
}
