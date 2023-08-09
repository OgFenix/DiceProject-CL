using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireButtons : MonoBehaviour
{
    [SerializeField]
    PlayerBehaviour player;
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
        if (player.health >= player.startingHealth / 2)
            player.health = player.startingHealth;
        else
            player.health += player.startingHealth / 2;
        campfirePanel.SetActive(false);
    }
}
