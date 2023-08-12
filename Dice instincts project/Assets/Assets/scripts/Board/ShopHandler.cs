using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    enum UpgradeType
    {
        Card,
        Relic
    }
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    GameObject scrollContainer;
    [SerializeField]
    DeckScrollMenu deckScrollMenu;
    [SerializeField]
    private PlayerMovement Player;
    [SerializeField]
    private GameObject ArchiveShopPrefab;
    [SerializeField]
    private GameObject ArchiveShopContainer;
    private List<Vector3Int> ArchiveShopID = new List<Vector3Int>();
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
    [SerializeField]
    private GameObject SoldRemovalInThisShop;
    private int RemoveCardCost;
    int ExistingShopIndex;
    bool IsInShop = false;
    public void OpenShop(int NumOfCards, int NumOfRelics)
    {
        IsInShop = true;
        PickCardManager.pickFor = PickCardManager.PickFor.buy;
        ExistingShopIndex = ArchiveShopID.IndexOf(Player.GetPlayerPositionInTilemap());//-1 if no related one is found
        if (ExistingShopIndex != -1)
            ReOpenExistingShop(ExistingShopIndex);
        else
            GenerateNewShop(NumOfCards, NumOfRelics);
    }
    private void ReOpenExistingShop(int ShopToReopenIndex)
    {
        GameObject ArchiveShopToReopen = ArchiveShopContainer.transform.GetChild(ShopToReopenIndex).gameObject;
        SoldRemovalInThisShop.SetActive(ArchiveShopToReopen.GetComponent<ShopArgs>().CardRemovalBoughtInThisShop);
        MoveArchiveShopToShelfs(ArchiveShopToReopen);
    }

    private void MoveArchiveShopToShelfs(GameObject ArchiveShopToReopen)
    {
        for (int i = 0; i < ArchiveShopToReopen.transform.childCount; i++)
        {
            int NumOfChildren = ArchiveShopToReopen.transform.GetChild(i).childCount;
            for (int j = 0; j < NumOfChildren; j++)
                ArchiveShopToReopen.transform.GetChild(i).GetChild(0).SetParent(transform.GetChild(i));
        }
    }    
    private void GenerateNewShop(int NumOfCards, int NumOfRelics)
    {
        AddUpgradeToShop(NumOfCards, cardsDictionary, CardPrefab, UpgradeType.Card);
        AddUpgradeToShop(NumOfRelics, relicDictionary, RelicPrefab, UpgradeType.Relic);
    }
    private void AddUpgradeToShop(int NumOf, FrameworkDictionary DictionaryOf, GameObject PrefabOf, UpgradeType type)
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
                NewItem.transform.GetChild(0).localScale = Vector3.one * 1 / 3;
            //creating a cost text for shop only
            GameObject ShopCost = Instantiate(CoinSystemCointainerPrefab);
            ShopCost.transform.SetParent(NewItem.transform);
            ShopCost.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            NewItem.CurrShopPrice = Random.Range(NewItem.ShopCostRange[0], NewItem.ShopCostRange[1] + 1); //get a random num from range
            ShopCost.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = NewItem.CurrShopPrice.ToString();
            GameObject SoldTag = Instantiate(SoldPrefab);
            SoldTag.transform.SetParent(NewItem.transform);
            SoldTag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            NewItem.SoldTag = SoldTag;
            NewItem.SoldTag.SetActive(false);
        }
    }

    public void CloseShop()
    {
        GameObject ShopInArchive;
        if (ExistingShopIndex == -1)
        {
            ShopInArchive = Instantiate(ArchiveShopPrefab);
            ShopInArchive.transform.SetParent(ArchiveShopContainer.transform);
            Vector3Int TileOfShop = Player.GetPlayerPositionInTilemap();
            ShopInArchive.name = TileOfShop.ToString();
            ArchiveShopID.Add(TileOfShop);
        }
        else
            ShopInArchive = ArchiveShopContainer.transform.GetChild(ExistingShopIndex).gameObject;
        MoveToArchiveShelfs(ShopInArchive);
        this.gameObject.SetActive(false);
        boardManager.StopOnTile = false;
        ShopInArchive.GetComponent<ShopArgs>().CardRemovalBoughtInThisShop = SoldRemovalInThisShop.activeSelf;
        SoldRemovalInThisShop.SetActive(false);
        IsInShop = false;
    }

    private void MoveToArchiveShelfs(GameObject ShopInArchive)
    {
        for (int i = 0; i < ShopInArchive.transform.childCount; i++)
        {
            int NumOfChildren = transform.GetChild(i).childCount;
            for (int j = 0; j < NumOfChildren; j++)
                transform.GetChild(i).GetChild(0).SetParent(ShopInArchive.transform.GetChild(i));
        }
    }

    public void OpenCardRemoval()
    {
        RemoveCardCost = 20;
        if (boardManager.Money < RemoveCardCost || SoldRemovalInThisShop.activeSelf == true)
            return;
        PickCardManager.pickFor = PickCardManager.PickFor.remove;
        scrollContainer.GetComponent<PickCardManager>().enabled = true;
        deckScrollMenu.moveDeckToScrollMenu();
        this.gameObject.SetActive(false);
    }
    public void CardRemovalBought()
    {
        this.gameObject.SetActive(true);
        boardManager.UpdateMoney(-RemoveCardCost);
        SoldRemovalInThisShop.SetActive(true);
    }
    public void CloseCardRemoval()
    {
        if (IsInShop)
        {
            this.gameObject.SetActive(true);
            PickCardManager.pickFor = PickCardManager.PickFor.buy;
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
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
