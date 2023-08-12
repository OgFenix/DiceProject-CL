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
    public TextMeshProUGUI CurHealthText;
    public TextMeshProUGUI CurBlockText;

    public virtual void UpdateHealth(int damage)
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
        newStatus.transform.localScale = Vector3.one * 0.5f;
        newStatus.GetComponent<Image>().sprite = statusesList[statusesList.Count - 1].statusImg;
        newStatus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = statusesList[statusesList.Count - 1].count.ToString();
    }
    private void updateStatuses()
    {
        for(int i = 0; i < statusesList.Count; i++)
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
        DestroyImmediate(statusContainer.transform.GetChild(ind).gameObject);
        statusesList.RemoveAt(ind);
    }

    public void ActivateEndOfTurnStatuses()
    {
        for (int i = 0; i < statusesList.Count; i++)
        {
            if (statusesList[i].GetType() == typeof(TimedStatuses))
            {
                if (statusesList[i].status == Status.poison)
                    UpdateHealth(statusesList[i].count);
                statusesList[i].count--;
                if (statusesList[i].count == 0)
                {
                    removeStatus(statusesList[i]);
                    i--;
                }
                    
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
