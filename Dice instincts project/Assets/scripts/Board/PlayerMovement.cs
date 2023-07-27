using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    GameObject PossibleEndingSquaresList;
    [SerializeField]
    DiceBehaviour Dice;
    [SerializeField]
    Tilemap tilemap;
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
    List<List<Vector3Int>> PossiblePaths = new List<List<Vector3Int>>();
    int ChosenPathInd = 0;
    void Start()
    {
        cellPlayerPosition = tilemap.WorldToCell(gameObject.transform.position);
    }

    public void initializePossibleEndingSqures(int ToMove)
    {
        PossibleEndingSqures.Clear();
        PossibleEndingSquresPos.Clear();
        GetAllPossibleEndingSqures(ToMove, cellPlayerPosition, new List<int>(), new List<Vector3Int>(),0);
        foreach (var PossibleEndingSqure in PossibleEndingSqures)
            PossibleEndingSquresPos.Add(tilemap.GetCellCenterWorld(PossibleEndingSqure));
        MarkPossibleEndingSqures();
    }

    private void GetAllPossibleEndingSqures(int ToMove, Vector3Int PosToCheck, List<int> NonViableDirInds,List<Vector3Int> Path, int depth)
    {
        depth++;
        Path.Add(PosToCheck);
        bool PathFound = false;
        if (ToMove == 0)
        {
            PossiblePaths.Add(Path);
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
                NewNonViableDirInds.Add(3 - i); // 3-i is the by design the opposite direction
                GetAllPossibleEndingSqures(ToMove - 1, PosToCheck, NewNonViableDirInds, Path, depth);
                Path = Path.Take(depth).ToList();
                NewNonViableDirInds.Clear();
            }
            PosToCheck = PosToCheck - MoveableDirections[i];
        }
        if (!PathFound)
        {
            foreach (int i in NonViableDirInds)
            {
                PosToCheck = PosToCheck + MoveableDirections[i];
                TileBase tile = tilemap.GetTile(PosToCheck);
                if (tile != null)
                {
                    PathFound = true;
                    NewNonViableDirInds.Add(3 - i); // 3-i is the by design the opposite direction
                    GetAllPossibleEndingSqures(ToMove - 1, PosToCheck, NewNonViableDirInds, Path, depth);
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
            for (int i = 0; i < PossibleEndingSquaresMarks.Count; i++)
            {
                if (PossibleEndingSquaresMarks[i].GetComponent<BoxCollider2D>().OverlapPoint(new Vector2(MousePos.x, MousePos.y)))
                {
                    gameObject.transform.position = PossibleEndingSquaresMarks[i].transform.position;
                    cellPlayerPosition = tilemap.WorldToCell(gameObject.transform.position);
                    foreach (var PossibleEndingSquareMark1 in PossibleEndingSquaresMarks)
                    {
                        GameObject.Destroy(PossibleEndingSquareMark1.gameObject);
                    }
                    ChosenPathInd = i;
                    StartCoroutine(GoAlongChosenPath());
                    PossibleEndingSquaresMarks.Clear();
                    break;
                }
            }
        }
    }

    IEnumerator GoAlongChosenPath()
    {
        foreach (var step in PossiblePaths[ChosenPathInd])
        {
            gameObject.transform.position = tilemap.GetCellCenterWorld(step);
            if (boardManager.IsContainingEnemy(step))
                while (boardManager.IsInCombat)
                    yield return null;
            yield return new WaitForSeconds(0.3f);
        }
        Dice.IsRollAllowed = true;
        boardManager.TurnIsOver();
        PossiblePaths.Clear();
    }
}
