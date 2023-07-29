using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : FrameworkOfObject
{
    public override int id { get; protected set; }
    public string relicName;
    public string relicDisc;
    public Sprite relicImage;
    public Classes relicForClass;
    public string scriptPath;

    public Relic(int id, string relicName, string relicDisc, Sprite relicImage, Classes relicForClass, string scriptPath)
    {
        this.id = id;
        this.relicName = relicName;
        this.relicDisc = relicDisc;
        this.relicImage = relicImage;
        this.relicForClass = relicForClass;
        this.scriptPath = scriptPath;
    }
    // type component = CardBehaviour;
}
