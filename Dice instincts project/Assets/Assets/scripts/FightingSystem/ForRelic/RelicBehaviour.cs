using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class RelicBehaviour : MonoBehaviour
{
    bool IsRelicInit = false;
    public int id { get; private set; }
    public string relicName;
    public string relicDisc;
    public Sprite relicSprite;
    public Classes relicForClass;
    public string scriptPath;
    private Relic thisRelic;
    private RelicDictionary relicDictionary;
    public TextMeshProUGUI relicNameText;
    public TextMeshProUGUI descriptionText;
    public Image relicImage;
    public Component RelicSpesificScript;
    private GameObject RelicDiscContainer;
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
    public void CreateRelic (int id)
    {
        relicImage = GetComponent<Image>();
        GetChildrenComponents();
        relicDictionary = GameObject.Find("GameDirector").GetComponent<RelicDictionary>();
        thisRelic = (Relic)relicDictionary.InitializeByID(id);
        //creating relic from thisRelic
        this.id = thisRelic.id;
        relicName = thisRelic.relicName;
        relicDisc = thisRelic.relicDisc;
        relicSprite = thisRelic.relicImage;
        relicForClass = thisRelic.relicForClass;
        scriptPath = thisRelic.scriptPath;
        relicImage.sprite = relicSprite;
        relicNameText.text = relicName;
        descriptionText.text = relicDisc;
        FindRelicSpesificScript();
        IsRelicInit = true;
    }

    private void FindRelicSpesificScript()
    {
        Assembly assembly = Assembly.Load("Assembly-CSharp");
        Type scriptType = assembly.GetType(scriptPath);
        if(scriptType != null )
        {
            RelicSpesificScript = gameObject.AddComponent(scriptType);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!IsRelicInit)
        {
            CreateRelic(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}