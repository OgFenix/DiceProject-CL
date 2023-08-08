using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public abstract void Create(int id,bool IsUpgraded = false);
    public abstract void ActivateEffect(EffectTiming timing);
    public List<FuncArgs> effects;
}
