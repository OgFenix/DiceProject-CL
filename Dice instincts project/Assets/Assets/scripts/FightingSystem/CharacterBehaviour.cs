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
        GameObject newStatus;
        newStatus = Instantiate(statusPrefab);
        newStatus.transform.SetParent(statusContainer.transform);
        newStatus.GetComponent<Image>().sprite = statusesList[statusesList.Count - 1].statusImg;
        newStatus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[statusesList.Count - 1].count.ToString();
    }
    private void updateStatuses()
    {
        for(int i = 0; i < statusContainer.transform.childCount; i++)
            statusContainer.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[i].count.ToString();
    }
    public void RemoveAllStatuses()
    {
        for (int i = 0; i < statusesList.Count; i++)
            removeStatus(statusesList[i]);
    }
    public void removeStatus(GeneralStatus cStatus)
    {
        int ind = statusesList.IndexOf(cStatus);
        Destroy(statusContainer.transform.GetChild(ind).gameObject);
        for (int i = ind + 1; i < statusesList.Count; i++)
            statusContainer.transform.GetChild(i).transform.position = new Vector3(statusContainer.transform.GetChild(i).transform.position.x - 2, statusContainer.transform.GetChild(i).transform.position.y, statusContainer.transform.GetChild(i).transform.position.z);
    }

    public void ActivateEndOfTurnStatuses()
    {
        foreach(GeneralStatus timedStatus in statusesList)
        {
            if (timedStatus.GetType() == typeof(TimedStatuses))
            {
                if (timedStatus.status == Status.poison)
                    UpdateHealth(timedStatus.count);
                timedStatus.count--;
                if (timedStatus.count == 0)
                    removeStatus(timedStatus);
            }
        }
        updateStatuses();
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
