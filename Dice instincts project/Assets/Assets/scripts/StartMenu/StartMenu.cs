using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject startMenu;
    [SerializeField]
    public GameObject settingsMenuStart;
    [SerializeField]
    public GameObject settingsMenuGame;
    [SerializeField]
    public GameObject boardGame;
    [SerializeField]
    public Button playBtn;
    [SerializeField]
    public Button settingsBtn;

    public void openSettingsFromStart()
    {
        settingsMenuStart.SetActive(true);
        playBtn.interactable = false;
        settingsBtn.interactable = false;
    }
    public void closeSettingsFromStart()
    {
        settingsMenuStart.SetActive(false);
        playBtn.interactable = true;
        settingsBtn.interactable = true;
    }
    public void openSettingsFromGame()
    {
        settingsMenuGame.SetActive(true);
    }
    public void closeSettingsFromGame()
    {
        settingsMenuGame?.SetActive(false);
    }
    public void exitGame()
    {
        boardGame.SetActive(false);
        startMenu.SetActive(true);
    }
    public void startGame()
    {
        startMenu.SetActive(false);
        boardGame.SetActive(true);
    }


}
