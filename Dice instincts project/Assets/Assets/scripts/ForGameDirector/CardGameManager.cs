using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    public event GameEvent StartOfTurn;
    public event GameEvent EndOfTurn;
    [SerializeField]
    public GameObject DiscardContainer;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject cardContainer;
    [SerializeField]
    public GameObject exhaustContainer;
    
    [SerializeField]
    TextMeshProUGUI deckAmount;
    [SerializeField]
    public TextMeshProUGUI DiscardAmount;
    [SerializeField]
    public TextMeshProUGUI exhaustAmount;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    public PlayerBehaviour player;

    public List<EnemyBehaviour> activeenemies;

    public List<GameObject> curFightDeck;

    public List<GameObject> discardPile = new List<GameObject>();
    public List<GameObject> exhaustPile = new List<GameObject>();
    OverallGameManager gameManager;
    [SerializeField]
    private StatusDictionary statusDictionary;

    public void endYourTurn()
    {
        player.CurManaToMaxMana();
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        foreach(var enemy in activeenemies)
        {
            yield return new WaitForSeconds(0.3f);
            enemy.EnemyAttack();
        }
    }

    public void DrawCard()
    {
        //Debug.Log(UnityEngine.Random.Range(0, deck.Count));
        if (curFightDeck.Count == 0 && discardPile.Count > 0)
            SwapDiscardPileAndDeck();
        if (curFightDeck.Count == 0)
            return;
        GameObject randcard = curFightDeck[UnityEngine.Random.Range(0, curFightDeck.Count)];
        curFightDeck.Remove(randcard);
        randcard.SetActive(true);
        randcard.transform.SetParent(hand.transform, false); 

    }    
    public void SwapDiscardPileAndDeck()
    {
        List<GameObject> tmp = discardPile;
        discardPile = curFightDeck;
        curFightDeck = tmp;
        DiscardAmount.text = discardPile.Count.ToString();
        deckAmount.text = curFightDeck.Count.ToString();
    }
    public void EffectOnEnemyTargeted(object sender, FuncArgs args)
    {
        args.FuncToRun(sender, args);
    }
    public void EffectOnSelf(object sender, FuncArgs args)
    {
        GameObject sendergameobject = (GameObject)sender;
        args.character = sendergameobject.GetComponent<CharacterBehaviour>();
        args.FuncToRun(sender, args);
    }

    public void EffectOnPlayer(object sender, FuncArgs args)
    {
            args.character = player;
            args.FuncToRun(sender, args);
    }
    public void AOE_Effect(object sender, FuncArgs args)
    {
        foreach (var enemy in activeenemies)
        {
            args.character = enemy;
            args.FuncToRun(sender, args);
        }
    }
    public void DrawCards(object sender, FuncArgs args)
    {
        for (int i = 0; i < args.EffectNum; i++) {
            if (curFightDeck.Count == 0 && discardPile.Count > 0)
                SwapDiscardPileAndDeck();
            if (curFightDeck.Count > 0)
            {
                GameObject cardToDraw = curFightDeck[0];
                curFightDeck.RemoveAt(0);
                cardToDraw.SetActive(true);
                cardToDraw.transform.SetParent(hand.transform, false);
            }
            else
            {
                //logic for no more cards
                Debug.Log("Can't Draw! no more cards");
            }
        }

    }
    public void DealDamage(object sender, FuncArgs args)
    {
        CharacterBehaviour curSender;
        curSender = ((GameObject)sender).GetComponent<EnemyBehaviour>();
        if (curSender == null)
            curSender = ((GameObject)sender).GetComponent<PlayerBehaviour>();

        bool isWeak = false;
        int curEffectNum = args.EffectNum;
        if (curSender.statusesList != null)
        {
            foreach(var status in curSender.statusesList)
            {
                if (status.status == Status.strength)
                    curEffectNum += status.count;
            }
            foreach (var status in curSender.statusesList)
            {
                if (status.status == Status.weak)
                    isWeak = true;
            }
        }
        if (isWeak)
            curEffectNum = (int)(curEffectNum * 0.75f);

        if (args.character.block >= curEffectNum)
            args.character.ChangeArmor(-1 * curEffectNum);
        else
        {
            curEffectNum -= args.character.block;
            args.character.SetArmor(0);
            args.character.UpdateHealth(curEffectNum);
        }
    }
    public void ApplyStatus(object sender, FuncArgs args)
    {
        for (int i = 0; i < args.character.statusesList.Count; i++)
        {
            if (args.character.statusesList[i].status == args.status)
            {
                args.character.statusesList[i].count += args.EffectNum;
                args.character.statusContainer.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = args.character.statusesList[i].count.ToString();

                return;
            }
        }
        args.character.statusesList.Add((GeneralStatus)statusDictionary.ListOfObject[(int)args.status]);
        args.character.statusesList[args.character.statusesList.Count - 1].count = args.EffectNum;
        args.character.addStatus();
    }
    public void GainBlock(object sender, FuncArgs args)
    {
        args.character.ChangeArmor(args.EffectNum);
    }
    
    public void CardsFromHandToContainer()
    {
        Vector3 OrgLocalScale;
        List<GameObject> Children = new List<GameObject>();
        for (int i = 0; i < hand.transform.childCount; i++)
            Children.Add(hand.transform.GetChild(i).gameObject);
        for (int i = 0; i < Children.Count; i++)
        {
            OrgLocalScale = Children[i].transform.localScale;
            Children[i].transform.SetParent(gameManager.cardContainer.transform);
            Children[i].transform.localScale = OrgLocalScale;
        }
        Children = new List<GameObject>();
        for (int i = 0; i < DiscardContainer.transform.childCount; i++)
            Children.Add(DiscardContainer.transform.GetChild(i).gameObject);
        for (int i = 0; i < Children.Count; i++)
        {
            OrgLocalScale = Children[i].transform.localScale;
            Children[i].transform.SetParent(gameManager.cardContainer.transform);
            Children[i].transform.localScale = OrgLocalScale;
        }
    }   
    
    public void ClearDiscardPile()
    {
        discardPile.Clear();
        DiscardAmount.text = "0";
    }
    public void ClearExhaustPile()
    {
        exhaustPile.Clear();
        exhaustAmount.text = "0";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameDirector").GetComponent<OverallGameManager>();
        curFightDeck = ListRandomizer<GameObject>.Randomize(gameManager.deck).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        deckAmount.text = curFightDeck.Count.ToString();
        if (activeenemies.Count > 0 && activeenemies.All(x => x.health < 0))
        {
            gameManager.CombatWon();
            activeenemies.Clear();
        }
        if (player.health < 0)
            gameManager.CombatLost();
    }
}
