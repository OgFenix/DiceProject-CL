using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FrameworkDictionary : MonoBehaviour
{

    public List<FrameworkOfObject> ListOfObject { get; private set; } = new List<FrameworkOfObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitList();
    }
    public abstract void InitList();
    public FrameworkOfObject InitializeByID(int cardId)
    {
        return ListOfObject[cardId];
    }
    public int GetRandomID(List<int> PossibleIDs)
    {
        return PossibleIDs[UnityEngine.Random.Range(0, PossibleIDs.Count - 1)];
    }
    public int GetRandomID()
    {
        return ListOfObject[UnityEngine.Random.Range(0, ListOfObject.Count - 1)].id;
    }
}
