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
    CardBehaviour NewCard;
    Upgrade NewUpgrade;
    int Money = 0;
    public bool IsInCombat { get; set; } = false;
    [SerializeField]
    public List<EnemyMovement> enemies = new List<EnemyMovement>();
    GameObject EnemyInCombat = null;
    bool didEnemyStartCurrFight = false;
    int NumberOfItemsToChooseFromChest = 3;
    int fixedratefromcointile = 20;

    private void Start()
    {
        discoverPanel.GetComponent<CanvasRenderer>().SetAlpha(0f); //make panel invinsible
        CombatButton.SetActive(false);
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
            case '3':
                PickCardManager.pickFor = PickCardManager.PickFor.add;
                SteppedOnChestTile();
                break;
            case '4':
                SteppedOnQuestionMarkTile();
                break;
            case '5':
                SteppedOnShopTile();
                break;

        }
    }

    private void SteppedOnCampfireTile()
    {
        scrollContainer.GetComponent<PickCardManager>().enabled = true;
        exitDeckMenuBtn.SetActive(false);
        deckScrollMenu.moveDeckToScrollMenu();
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
            rand = Random.Range(1, 3);
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

    }

    private void UpdateMoney(int ToAdd)
    {
        Money += ToAdd;
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
        Vector3Int cellPlayerPosition = new Vector3Int();
        foreach (var step in ChosenPath)
        {
            if (IsContainingEnemy(step))
                while (IsInCombat)
                    yield return null;
            playerMovement.transform.position = tilemap.GetCellCenterWorld(step);
            cellPlayerPosition = step;
            yield return new WaitForSeconds(0.3f);
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
