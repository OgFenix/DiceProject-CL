using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus 
{
    public Status status;
    public int amountOfTurns;
    public CharacterStatus(Status status, int amountOfTurns)
    {
        this.status = status;
        this.amountOfTurns = amountOfTurns;
    }
}
