using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PassiveStatuses : GeneralStatus
{
    public PassiveStatuses(Status status, Sprite statusImg)
    {
        this.status = status;
        this.statusImg = statusImg;
        id = (int)status;
    }

   
}
