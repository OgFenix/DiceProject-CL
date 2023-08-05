using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

abstract public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    public GameObject statusPrefab;
    public List<GeneralStatus> statusesList = new List<GeneralStatus>();
    [SerializeField]
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
                statusContainer.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[0].count.ToString();
                lastStatusPos = statusContainer.transform.GetChild(0).position;
            }
            else
            {
                Vector3 newPos = lastStatusPos;
                newPos.x += 40;
                GameObject newStatus;
                newStatus = Instantiate(statusPrefab);
                newStatus.transform.localScale = new Vector3(2.3106f, 2.3106f, 2.3106f);
                newStatus.SetActive(true);
                newStatus.transform.SetParent(statusContainer.transform);
                newStatus.transform.position = newPos;
                newStatus.GetComponent<Image>().sprite = statusesList[statusesList.Count - 1].statusImg;
                newStatus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[statusesList.Count - 1].count.ToString();
                lastStatusPos = newPos;

            }
        }
    }
    public void RemoveAllStatuses()
    {
        statusContainer.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 1; i < statusesList.Count; i++)
            removeStatus(statusesList[i]);
    }
    public void removeStatus(GeneralStatus cStatus)
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
