using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public abstract void Create(int id,bool IsUpgraded = false);
    public abstract void ActivateEffect(EffectTiming timing);
    protected OverallGameManager overallGameManager;
    protected CardGameManager cardGameManager;
    protected BoardManager boardManager;
    public List<FuncArgs> effects;
    public int[] ShopCostRange;
    public int CurrShopPrice = 0;
    public void BuyThis() => boardManager.UpdateMoney(-CurrShopPrice);
    public bool IsBuyable()
    { 
        if(boardManager.Money < CurrShopPrice)
            return false;
        return true;
    }
}
