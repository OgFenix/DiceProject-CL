using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDictionary : FrameworkDictionary
{ 
    string Path = "StatusesSprites/";

    private void AddTimedToList(Status status)
    {
        ListOfObject.Add(new TimedStatuses(status, Resources.Load<Sprite>(Path + status.ToString())));
    }
    private void AddPassiveToList(Status status)
    {
        ListOfObject.Add(new PassiveStatuses(status, Resources.Load<Sprite>(Path + status.ToString())));
    }
    public override void InitList()
    {
        AddTimedToList(Status.poison);
        AddTimedToList(Status.weak);
        AddTimedToList(Status.frail);
        AddPassiveToList(Status.strength);
        AddPassiveToList(Status.dexterity);
    }
}
