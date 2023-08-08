using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class DeckScrollMenu : MonoBehaviour
{
    [SerializeField]
    GameObject scrollListViewPort;
    [SerializeField]
    GameObject scrollMenuContainer;
    [SerializeField]
    GameObject overallCardContainer;
    [SerializeField]
    PickCardManager pickCardManager;
    public List<GameObject> deck;
    public bool isForUpgrade = false;
    // Start is called before the first frame update
    public void moveDeckToScrollMenu()
    {
        int childCount = overallCardContainer.transform.childCount;
        scrollListViewPort.SetActive(true);
        for (int i = 0; i < childCount; i++)
        {
            Transform child = overallCardContainer.transform.GetChild(0);
            child.SetParent(scrollMenuContainer.transform);
            child.gameObject.SetActive(true);
        }
    }
    public void moveDeckToOverallContainer()
    {
        int childCount = scrollMenuContainer.transform.childCount;
        scrollListViewPort.SetActive(false);
        for (int i = 0; i < childCount; i++)
        {
            Transform child = scrollMenuContainer.transform.GetChild(0);
            child.SetParent(overallCardContainer.transform);
            child.gameObject.SetActive(false);
        }
    }
}
