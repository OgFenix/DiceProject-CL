using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

abstract public class FrameworkDictionary : MonoBehaviour
{

    public List<FrameworkOfObject> ListOfObject { get; private set; } = new List<FrameworkOfObject>();
    public CardGameManager cardGameManager;
    public BoardManager boardManager;
    // Start is called before the first frame update
    void Awake()
    {
        cardGameManager = GameObject.Find("GameDirector").GetComponent<CardGameManager>();
        boardManager = GameObject.Find("GameDirector").GetComponent<BoardManager>();
        InitList();
    }
    public abstract void InitList();
    public FrameworkOfObject InitializeByID(int cardId)
    {
        return ListOfObject[cardId];
    }
    public int GetRandomID(List<int> PossibleIDs)
    {
        return PossibleIDs[UnityEngine.Random.Range(0, PossibleIDs.Count)];
    }
    public int GetRandomID()
    {
        return ListOfObject[UnityEngine.Random.Range(0, ListOfObject.Count)].id;
    }
    public int[] GetRandomUniqueIDs(int num)
    {
        num = math.min(num, ListOfObject.Count);
        int[] uniqueIDs = new int[num];
        int ToBeAdded;
        for (int i = 0; i < num; i++)
        {
            ToBeAdded = ListOfObject[UnityEngine.Random.Range(0, ListOfObject.Count)].id;
            if (!uniqueIDs.Contains(ToBeAdded))
                uniqueIDs[i] = ToBeAdded;
        }
        return uniqueIDs;
    }
}
