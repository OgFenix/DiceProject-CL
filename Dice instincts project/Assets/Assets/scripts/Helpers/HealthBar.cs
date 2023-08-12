using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetHealth(int health)
    {
        slider.value = health;
        this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Health: " + slider.value.ToString() + " / " + slider.maxValue.ToString();
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    private void Start()
    {
        slider.interactable = false;

    }
    private void Update()
    {
        if (MouseOverUI.IsMouseOverGameObject(gameObject.transform.GetChild(2).gameObject))
        {
            GameObject desc = this.transform.GetChild(0).gameObject;
            desc.SetActive(true);
        }
        else
        {
            GameObject desc = this.transform.GetChild(0).gameObject;
            desc.SetActive(false);
        }
    }
}
