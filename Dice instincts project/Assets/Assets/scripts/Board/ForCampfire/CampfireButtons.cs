using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireButtons : MonoBehaviour
{
    [SerializeField]
    PlayerBehaviour player;
    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    GameObject campfirePanel;
    [SerializeField]
    GameObject scrollContainer;
    [SerializeField]
    GameObject exitDeckMenuBtn;
    [SerializeField]
    DeckScrollMenu deckScrollMenu;

    public void upgradeACard()
    {
        scrollContainer.GetComponent<PickCardManager>().enabled = true;
        exitDeckMenuBtn.SetActive(false);
        deckScrollMenu.moveDeckToScrollMenu();
        campfirePanel.SetActive(false);
    }
    public void heal()
    {
        player.HealHalfHealth();
        campfirePanel.SetActive(false);
        boardManager.StopOnTile = false;
    }
}
