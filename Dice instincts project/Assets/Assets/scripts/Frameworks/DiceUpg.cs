using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUpg : FrameworkOfObject
{
    public override int id { get; protected set; }
    public string Upg_name;
    public string description;
    public Sprite upgImage;
    public Classes DiceUpgForClass = Classes.Warrior;
    public bool ToRandomlyAssign;
    public List<int> DiceFaces;
    public List<FuncArgs> effects;

    public DiceUpg(int id, string upg_name, string description, Sprite upgImage, bool toRandomlyAssign, List<int> diceFaces, List<FuncArgs> effects)
    {
        this.id = id;
        Upg_name = upg_name;
        this.description = description;
        this.upgImage = upgImage;
        ToRandomlyAssign = toRandomlyAssign;
        DiceFaces = diceFaces;
        this.effects = effects;
    }
}
