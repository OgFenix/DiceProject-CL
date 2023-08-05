using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimedStatuses : GeneralStatus
{
    public TimedStatuses(Status status, Sprite statusImg)
    {
        this.status = status;
        this.statusImg = statusImg;
        id = (int)status;
    }
}
