using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    public event GameEvent StartOfTurn;
    public event GameEvent EndOfTurn;
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject cardContainer;
    
    [SerializeField]
    TextMeshProUGUI deckAmount;
    [SerializeField]
    public TextMeshProUGUI DiscardAmount;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private PlayerBehaviour player;

    public List<EnemyBehaviour> activeenemies;

    public List<GameObject> curFightDeck;

    public List<GameObject> discardPile = new List<GameObject>();
    OverallGameManager gameManager;

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
        bool isWeak = false;
        int curEffectNum = args.EffectNum;
        if (args.character.statusesList != null)
            foreach(var status in args.character.statusesList)
            {
                if(status.status == Status.weak)
                    isWeak = true;

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
        for(int i = 0; i< args.character.statusesList.Count; i++)
        {
            if (args.character.statusesList[i].status == args.status)
            {
                args.character.statusesList[i].amountOfTurns += args.EffectNum;
                return;
            }
        }
        args.character.statusesList.Add(new CharacterStatus(args.status, args.EffectNum));
    }
    public void GainBlock(object sender, FuncArgs args)
    {
        args.character.ChangeArmor(args.EffectNum);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameDirector").GetComponent<OverallGameManager>();
        curFightDeck = ListRandomizer<GameObject>.Randomize(gameManager.deck);
    }

    // Update is called once per frame
    void Update()
    {
        deckAmount.text = curFightDeck.Count.ToString();
    }
}
