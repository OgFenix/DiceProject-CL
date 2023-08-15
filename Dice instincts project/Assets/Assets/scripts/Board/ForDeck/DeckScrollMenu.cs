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
    [SerializeField]
    GameObject exitBtn;
    public List<GameObject> deck;
    public bool isForUpgrade = false;
    [SerializeField]
    GameObject cardPrefab;
    Vector3 cardScale = new Vector3(0.7f, 0.7f, 1);
    [SerializeField]
    GameObject cameraObject;
    [SerializeField]
    BoardManager boardManager;
    // Start is called before the first frame update
    public void moveDeckToScrollMenu()
    {
        int curChild = 0;   
        cameraObject.transform.GetComponent<Board_CameraDrag>().enabled = false;
        int childCount = overallCardContainer.transform.childCount;
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
        ExpandChildrenToCell.SetChildrenToCellSize(scrollMenuContainer,1.5f,1.5f);
    }
    public void moveDeckToOverallContainer()
    {
        cameraObject.transform.GetComponent<Board_CameraDrag>().enabled = true;
        int childCount = scrollMenuContainer.transform.childCount;
        scrollListViewPort.SetActive(false);
        for (int i = 0; i < childCount; i++)
        {
            Transform child = scrollMenuContainer.transform.GetChild(0);
            child.SetParent(overallCardContainer.transform);
            child.gameObject.SetActive(false);
            child.transform.localScale = cardScale;
        }
    }
}
