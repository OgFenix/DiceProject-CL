using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuringCombatStats : MonoBehaviour
{
    [SerializeField]
    PlayerBehaviour playerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour.CurHealthText = GameObject.FindGameObjectWithTag("CombatHealth").GetComponent<TextMeshProUGUI>();
        playerBehaviour.CurBlockText = GameObject.FindGameObjectWithTag("CombatArmor").GetComponent<TextMeshProUGUI>();
    }

}
