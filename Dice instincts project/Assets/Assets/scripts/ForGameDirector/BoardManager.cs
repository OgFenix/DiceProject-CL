using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    
    [SerializeField]
    CardsDictionary cardsDictionary;
    [SerializeField]
    RelicDictionary relicDictionary;
    [SerializeField]
    GameObject discoverPanel;
    int discover_SizeMultiplayer = 4;
    [SerializeField]
    DiceBehaviour Dice;
    [SerializeField]
    GameObject CombatButton;
    [SerializeField]
    TextMeshProUGUI CoinText;
    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    GameObject relicPrefab;
    [SerializeField]
    CardGameManager cardGameManager;
    [SerializeField]
    OverallGameManager overallGameManager;
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    GameObject deckContainer;
    [SerializeField]
    DeckScrollMenu deckScrollMenu;
    [SerializeField]
    GameObject scrollContainer;
    [SerializeField]
    GameObject exitDeckMenuBtn;
    [SerializeField]    
    private List<TileBase> _regularTiles;
    [SerializeField]
    GameObject campfirePanel;
    [SerializeField]
    PlayerBehaviour player;
    [SerializeField]
    HealthBar healthBar;
    [SerializeField]
    GameObject ShopPanel;
    CardBehaviour NewCard;
    Upgrade NewUpgrade;
    public int Money { get;private set; } = 0;
    public bool IsInCombat { get; set; } = false;
    public bool StopOnTile = false;
    [SerializeField]
    public List<EnemyMovement> enemies = new List<EnemyMovement>();
    GameObject EnemyInCombat = null;
    //bool didEnemyStartCurrFight = false;
    int NumberOfItemsToChooseFromChest = 3;
    int fixedratefromcointile = 20;

    private void Start()
    {
        discoverPanel.GetComponent<CanvasRenderer>().SetAlpha(0f); //make panel invinsible
        CombatButton.SetActive(false);
        healthBar.SetHealth(player.health);

    }
    public void TurnIsOver(TileBase endingTile)
    {
        Dice.IsRollAllowed = true;
        ActivateEndingSquare(endingTile);
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.DecideMove();
        }
    }

    private void ActivateEndingSquare(TileBase endingTile)
    {
        switch (endingTile.name[12])    
        {
            case '0':
                PickCardManager.pickFor = PickCardManager.PickFor.upgrade;
                SteppedOnCampfireTile();
                break;
            case '1':
                SteppedOnCoinTile();
                break;
            case '2':
                SteppedOnFreeTurnTile();
                break;
            case '6':
                PickCardManager.pickFor = PickCardManager.PickFor.add;
                SteppedOnChestTile();
                break;
            case '7':
                SteppedOnQuestionMarkTile();
                break;
            case '8':
                SteppedOnShopTile();
                break;

        }
    }

    private void SteppedOnCampfireTile()
    {
        campfirePanel.SetActive(true);
    }
    private void SteppedOnCoinTile()
    {
        UpdateMoney(fixedratefromcointile);
    }
    private void SteppedOnFreeTurnTile()
    {

    }
    private void SteppedOnChestTile()
    {
        //List<int> possibleIDs = new List<int>();
        FrameworkDictionary dictionary;
        GameObject prefab;
        int rand;
        /*for (int i = 0; i < cardsDictionary.ListOfObject.Count; i++)
            possibleIDs.Add(i); */
        for (int i = 0; i < NumberOfItemsToChooseFromChest; i++)
        {
            rand = UnityEngine.Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    dictionary = cardsDictionary;
                    prefab = cardPrefab;
                    break;
                case 2:
                    dictionary = relicDictionary;
                    prefab = relicPrefab;
                    break;
                default:
                    Debug.Log("this means that you are outside of bounds of picking an upgrade for chest, fix it!");
                    dictionary = cardsDictionary;
                    prefab = cardPrefab;
                    break;
            }
            int newCardInDiscoverId = dictionary.GetRandomID();
            //possibleIDs.Remove(newCardInDiscoverId);
            NewUpgrade = Instantiate(prefab).GetComponent<Upgrade>();
            NewUpgrade.Create(newCardInDiscoverId);
            NewUpgrade.transform.SetParent(discoverPanel.transform);
            if(rand == 1)
                NewUpgrade.transform.localScale = Vector3.Scale(NewUpgrade.transform.localScale, new Vector3(discover_SizeMultiplayer, discover_SizeMultiplayer,1));
            if (rand == 2)
                NewUpgrade.transform.GetChild(0).localScale = Vector3.one;
        }
    }
    private void SteppedOnQuestionMarkTile()
    {

    }
    private void SteppedOnShopTile()
    {
        ShopPanel.SetActive(true);
        ShopPanel.GetComponent<ShopHandler>().OpenShop(3,3);
    }

    public void UpdateMoney(int Change)
    {
        Money += Change;
        CoinText.text = Money.ToString();
    }
    public bool IsContainingEnemy(Vector3Int pos)
    {
        foreach (EnemyMovement enemy in enemies)
            if(pos == enemy.EnemyCellPos)
            {
                overallGameManager.EnterCombat(enemy);
                return true;
            }
        return false;
    }
    public int GetCurrentDiceRoll()
    {
        return Dice.currentface;
    }

    private void Update()
    {

    }
    public void StartGoAlongPath(List<Vector3Int> ChosenPath, Tilemap tilemap)
    {
        StartCoroutine(GoAlongChosenPath(ChosenPath, tilemap));
    }
    public IEnumerator GoAlongChosenPath(List<Vector3Int> ChosenPath,Tilemap tilemap)
    {
        TileBase tile;
        Vector3Int cellPlayerPosition = new Vector3Int();
        foreach (var step in ChosenPath)
        {
            if (IsContainingEnemy(step))
                while (IsInCombat)
                    yield return null;
            playerMovement.transform.position = tilemap.GetCellCenterWorld(step);
            cellPlayerPosition = step;
            tile = tilemap.GetTile(step);
            if (tile == null)
            {
                yield return new WaitForSeconds(0.3f);
                break;
            }
            TileBase TileToTransform;
            int TileID;
            if (tile.name.Length == 13)
                TileID = int.Parse(tile.name.Substring(12, 1).ToString());
            else //14
                TileID = int.Parse(tile.name.Substring(12, 2).ToString());
            switch (TileID)
            {
                case 3:
                    TileToTransform = _regularTiles[0];
                    PickCardManager.pickFor = PickCardManager.PickFor.upgrade;
                    SteppedOnCampfireTile();
                    StopOnTile = true;
                    break;
                case 4:
                    TileToTransform = _regularTiles[1];
                    SteppedOnCoinTile();
                    break;
                case 5:
                    TileToTransform = _regularTiles[2];
                    SteppedOnFreeTurnTile();
                    break;
                case 9:
                    TileToTransform = _regularTiles[3];
                    PickCardManager.pickFor = PickCardManager.PickFor.add;
                    SteppedOnChestTile();
                    StopOnTile = true;
                    break;
                case 10:
                    TileToTransform = _regularTiles[4];
                    SteppedOnQuestionMarkTile();
                    break;
                case 11:
                    TileToTransform = _regularTiles[5];
                    StopOnTile = true;
                    SteppedOnShopTile();
                    break;
                default:
                    TileToTransform = tile;
                    break;
            }
            while(StopOnTile)
                yield return null;
            yield return new WaitForSeconds(0.3f);
            tilemap.SetTile(step, TileToTransform);
        }
        playerMovement.UpdatedPos();
        TurnIsOver(tilemap.GetTile(cellPlayerPosition));
    }

    public void CombatButtonPressed()
    {
            if (EnemyInCombat != null)
            {
                enemies.Remove(EnemyInCombat.GetComponent<EnemyMovement>());
                GameObject.Destroy(EnemyInCombat);
            }
            CombatButton.SetActive(false);
            IsInCombat = false;
    }    
}
