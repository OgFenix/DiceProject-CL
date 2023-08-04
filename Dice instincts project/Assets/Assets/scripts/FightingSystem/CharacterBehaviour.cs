using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

abstract public class CharacterBehaviour : MonoBehaviour
{
    GameObject statusPrefab;
    public List<CharacterStatus> statusesList;
    public GameObject statusContainer;
    public string characterName;
    public int startingHealth;
    public int health;
    public int block;
    protected TextMeshProUGUI CurHealthText;
    protected TextMeshProUGUI CurBlockText;
    public Vector3 lastStatusPos;

    public void UpdateHealth(int damage)
    {
        health -= damage;
        CurHealthText.text = health.ToString();
    }
    public void ChangeArmor(int num) //give negative number for reduction and positive to increase
    {
        block += num;
        CurBlockText.text = block.ToString();
    }
    public void SetArmor(int num)
    {
        block = num;
        CurBlockText.text = block.ToString();
    }
    public void addStatus()
    {
        if (statusesList.Count > 0)
        {
            if (statusesList.Count == 1)
            {
                statusContainer.transform.GetChild(0).gameObject.SetActive(true);
                statusContainer.transform.GetChild(0).GetComponent<Image>().sprite = statusesList[0].statusImg;
                statusContainer.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[0].amountOfTurns.ToString();
                lastStatusPos = statusContainer.transform.GetChild(0).position;
            }
            else
            {
                Vector3 newPos = lastStatusPos;
                newPos.x += 2;
                GameObject newStatus;
                newStatus = Instantiate(statusPrefab);
                newStatus.SetActive(true);
                newStatus.transform.SetParent(statusContainer.transform);
                newStatus.transform.position = newPos;
                newStatus.GetComponent<Image>().sprite = statusesList[statusesList.Count - 1].statusImg;
                newStatus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[statusesList.Count - 1].amountOfTurns.ToString();
                lastStatusPos = newPos;

            }
        }
    }
    public void removeStatus(CharacterStatus cStatus)
    {
        int ind = statusesList.IndexOf(cStatus);
        Destroy(statusContainer.transform.GetChild(ind).gameObject);
        for (int i = ind + 1; i < statusesList.Count; i++)
            statusContainer.transform.GetChild(i).transform.position = new Vector3(statusContainer.transform.GetChild(i).transform.position.x - 2, statusContainer.transform.GetChild(i).transform.position.y, statusContainer.transform.GetChild(i).transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
