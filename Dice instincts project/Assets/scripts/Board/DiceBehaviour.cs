using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceBehaviour : MonoBehaviour
{
    [SerializeField]
    Collider2D DiceCollider;
    [SerializeField]
    TextMeshProUGUI FaceText;
    [SerializeField]
    int[] DiceFaces = { 1, 2, 3, 4, 5, 6 };
    bool isRolling = false;
    int currentface;
    [SerializeField]
    PlayerMovement PlayerMovementManager;
    public bool IsRollAllowed = true;
    // Start is called before the first frame update
    void Start()
    {
        currentface = DiceFaces[Random.Range(0, 6)];
        FaceText.text = $"{currentface}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            currentface = DiceFaces[Random.Range(0, 6)];
            FaceText.text = $"{currentface}";
            //Debug.Log("Current Face Is: " + currentface);
        }
        if (IsRollAllowed && Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = MousePositionCalc.GetMousePositionInWorld();
            // Check if the dice is clicked

            if (DiceCollider.OverlapPoint(new Vector2(MousePos.x, MousePos.y)))
            {
                if (isRolling)
                {
                    IsRollAllowed = false;
                    PlayerMovementManager.initializePossibleEndingSqures(currentface);
                }
                // Toggle the dice rolling state
                isRolling = !isRolling;
            }
        } 
    }
}
