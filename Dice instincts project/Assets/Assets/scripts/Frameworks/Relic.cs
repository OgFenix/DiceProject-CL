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
    public List<FuncArgs> effects;

    public Relic(int id, string relicName, string relicDisc, Sprite relicImage, Classes relicForClass, List<FuncArgs> effects)
    {
        this.id = id;
        this.relicName = relicName;
        this.relicDisc = relicDisc;
        this.relicImage = relicImage;
        this.relicForClass = relicForClass;
        this.effects = effects;
    }
    // type component = CardBehaviour;
}
