using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ShopGenerator : MonoBehaviour
{
    enum UpgradeType
    {
        Card,
        Relic,

    }
    [SerializeField]
    private GameObject SoldPrefab;
    [SerializeField]
    private GameObject CoinSystemCointainerPrefab;
    [SerializeField]
    private CardsDictionary cardsDictionary;
    [SerializeField]
    private GameObject CardPrefab;
    [SerializeField]
    private RelicDictionary relicDictionary;
    [SerializeField]
    private GameObject RelicPrefab;
    public void GenerateShop(int NumOfCards,int NumOfRelics)
    {
        PickCardManager.pickFor = PickCardManager.PickFor.buy;
        AddUpgradeToShop(NumOfCards, cardsDictionary, CardPrefab,UpgradeType.Card);
        AddUpgradeToShop(NumOfRelics, relicDictionary, RelicPrefab, UpgradeType.Relic);
    }
    private void AddUpgradeToShop(int NumOf,FrameworkDictionary DictionaryOf,GameObject PrefabOf,UpgradeType type)
    {
        Upgrade NewItem;
        int[] generatedIDs = DictionaryOf.GetRandomUniqueIDs(NumOf);
        foreach (int ID in generatedIDs)
        {
            NewItem = Instantiate(PrefabOf).GetComponent<Upgrade>();
            NewItem.Create(ID);
            NewItem.transform.SetParent(transform.GetChild((int)type));
            ExpandChildrenToCell.SetGameObjectToNewSize(NewItem.gameObject, 3f, 3f);
            if (type == UpgradeType.Relic)
                NewItem.transform.GetChild(0).localScale = Vector3.one * 1/3;
            //creating a cost text for shop only
            GameObject ShopCost = Instantiate(CoinSystemCointainerPrefab);
            ShopCost.transform.SetParent(NewItem.transform);
            ShopCost.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            NewItem.CurrShopPrice = Random.Range(NewItem.ShopCostRange[0], NewItem.ShopCostRange[1] + 1); //get a random num from range
            ShopCost.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = NewItem.CurrShopPrice.ToString();
            GameObject SoldTag = Instantiate(SoldPrefab);
            SoldTag.transform.SetParent(NewItem.transform);
            SoldTag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }

    /*private void moveDeckToScrollMenu()
    {
        int curChild = 0;
        Camera.main.transform.GetComponent<Board_CameraDrag>().enabled = false;
        scrollListViewPort.SetActive(true);
        for (int i = 0; i < childCount; i++)
        {
            Transform child = overallCardContainer.transform.GetChild(curChild);
            if (child.GetComponent<CardBehaviour>().isUpgraded == false || exitBtn.activeSelf == true)
            {
                child.SetParent(scrollMenuContainer.transform);
                child.gameObject.SetActive(true);
            }
            else
                curChild++;
        }
        ExpandChildrenToCell.SetChildrenToCellSize(scrollMenuContainer); 
    } */
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
