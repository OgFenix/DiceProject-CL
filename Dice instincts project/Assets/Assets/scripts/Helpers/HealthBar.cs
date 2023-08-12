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
        Vector2 mousePosition = Input.mousePosition;

        // Convert mouse position to local position within the canvas
        Vector2 localMousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), mousePosition, null, out localMousePos))
        {
            if (GetComponent<RectTransform>().rect.Contains(localMousePos))
            {
                //Debug.Log("Mouse is over the specific GameObject: " + this.name);
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
}
