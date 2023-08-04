using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDictionary : FrameworkDictionary
{
    string Path = "StatusesSprites/";

    private void AddToList(Status status)
    {
        ListOfObject.Add(new CharacterStatus(status, Resources.Load<Sprite>(Path + status.ToString())));
    }
    public override void InitList()
    {
        AddToList(Status.poison);
        AddToList(Status.weak);
        AddToList(Status.frail);
        AddToList(Status.strength);
        AddToList(Status.dexterity);
    }
}
