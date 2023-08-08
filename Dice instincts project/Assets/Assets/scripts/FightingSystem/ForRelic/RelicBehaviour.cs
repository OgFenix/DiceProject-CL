using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class RelicBehaviour : Upgrade
{
    bool IsRelicInit = false;
    public int id { get; private set; }
    public string relicName;
    public string relicDisc;
    public Sprite relicSprite;
    public Classes relicForClass;
    //public List<FuncArgs> effects;
    private Relic thisRelic;
    private RelicDictionary relicDictionary;
    public TextMeshProUGUI relicNameText;
    public TextMeshProUGUI descriptionText;
    public Image relicImage;
    public Component RelicSpesificScript;
    private GameObject RelicDiscContainer;
    private OverallGameManager overallGameManager;
    private void GetChildrenComponents()
    {
        RelicDiscContainer = transform.GetChild(0).gameObject;
        for (int i = 0; i < RelicDiscContainer.transform.childCount; i++)
        {
            Transform childTransform = RelicDiscContainer.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            switch (childObject.tag)
            {
                case "Card_Name":
                    relicNameText = childObject.GetComponent<TextMeshProUGUI>();
                    break;
                case "CardDescription":
                    descriptionText = childObject.GetComponent<TextMeshProUGUI>();
                    break;
            }
        }
    }
    public override void Create(int id)
    {
        relicImage = GetComponent<Image>();
        GetChildrenComponents();
        GameObject gamedirector = GameObject.Find("GameDirector");
        overallGameManager = gamedirector.GetComponent<OverallGameManager>();
        relicDictionary = gamedirector.GetComponent<RelicDictionary>();
        thisRelic = (Relic)relicDictionary.InitializeByID(id);
        //creating relic from thisRelic
        this.id = thisRelic.id;
        relicName = thisRelic.relicName;
        relicDisc = thisRelic.relicDisc;
        relicSprite = thisRelic.relicImage;
        relicForClass = thisRelic.relicForClass;
        effects = thisRelic.effects;
        relicImage.sprite = relicSprite;
        relicNameText.text = relicName;
        descriptionText.text = relicDisc;
        foreach (var effect in effects)
            if (effect.Timing == EffectTiming.Immidiate)
                overallGameManager.ActivateEffect(this.gameObject, effect);
            else
                overallGameManager.SubscribeToReleventEvent(effect.Timing, ActivateEffect);
        IsRelicInit = true;
    }
    public override void ActivateEffect(EffectTiming Timing)
    {
        foreach (var effect in effects)
            if (effect.Timing == Timing)
                overallGameManager.ActivateEffect(this.gameObject, effect);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!IsRelicInit)
        {
            Create(0);
        }
    }

    // Update is called once per frame
    void Update()
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
