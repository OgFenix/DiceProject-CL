using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public abstract void Create(int id,bool IsUpgraded = false);
    public abstract void ActivateEffect(EffectTiming timing);
    protected OverallGameManager overallGameManager;
    protected CardGameManager cardGameManager;
    protected BoardManager boardManager;
    public GameObject SoldTag; //only isnt null in shop, turn on and off to know if already soldout or not
    public List<FuncArgs> effects;
    public int[] ShopCostRange;
    public int CurrShopPrice = 0;
    public void BuyThis() => boardManager.UpdateMoney(-CurrShopPrice);
    public bool IsBuyable()
    { 
        if(boardManager.Money < CurrShopPrice || SoldTag.activeSelf == true)
            return false;
        return true;
    }
}
