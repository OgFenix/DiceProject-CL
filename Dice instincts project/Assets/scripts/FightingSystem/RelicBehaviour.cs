using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicBehaviour : MonoBehaviour
{
    private string relicName;

    public RelicBehaviour(string relicName)
    {
        this.relicName = relicName;
    }

    //get and set functions
    #region
    public void setRelicName(string relicName)
    {
        this.relicName = relicName;
    }
    public string getRelicName()
    {
        return relicName;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
