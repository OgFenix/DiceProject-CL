using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GeneralStatus : FrameworkOfObject
{
    public override int id { get; protected set; }
    public Status status;
    public int count = 0;
    public Sprite statusImg;
}
