using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatus: FrameworkOfObject
{
    public override int id { get; protected set; }
    public Status status;
    public int amountOfTurns = 0;
    public Sprite statusImg;


    public CharacterStatus(Status status, Sprite statusImg)
    {
        this.status = status;
        this.statusImg = statusImg;
        id = (int)status;
    }
}
