using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DiceBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera _canvasCam;
    [SerializeField]
    PlayerMovement PlayerMovementManager;
    [SerializeField]
    private float rotationSpeedUpMin = 1250f;
    [SerializeField]
    private float rotationSpeedUpMax = 750f;
    [SerializeField]
    private float rotationSpeedLeftMin = 750f;
    [SerializeField]
    private float rotationSpeedLeftMax = 1250f;
    private int[] _faceVaules = new int[6] { 1, 2, 3, 4, 5, 6};
    private Quaternion[] _faceDirs = new Quaternion[6] {Quaternion.Euler(0, 0, 0),Quaternion.Euler(0, 90, 0),Quaternion.Euler(-90, 0, 0),Quaternion.Euler(90, 0, 0),Quaternion.Euler(0, -90, 0),Quaternion.Euler(0, 180, 0)};
    public int CurrDiceValue;
    public bool IsRollAllowed = true;
    private bool _isRolling = false;
    private string diceRes = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _canvasCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            // Check if the hit object is the GameObject you want to interact with
            if ((_isRolling == true || IsRollAllowed == true) && hitObject.CompareTag("Dice"))
            {
                IsRollAllowed = false;
                _isRolling = !_isRolling;
                if (!_isRolling)
                    StopRolling();
            }
        }
        if(_isRolling)
        {
            transform.Rotate(Vector3.up * Random.Range(rotationSpeedUpMin, rotationSpeedUpMax + 1) * Time.deltaTime + Vector3.left * Random.Range(rotationSpeedLeftMin, rotationSpeedLeftMax + 1) * Time.deltaTime);
        }
    }
    private void StopRolling()
    {
        int FaceID;
        if (diceRes!= "")
            FaceID = int.Parse(diceRes);
        else
        {
            FaceID = Random.Range(0, 6);
            transform.localRotation = _faceDirs[FaceID];
            CurrDiceValue = _faceVaules[FaceID];
        }
        transform.localPosition = Vector3.zero;
        PlayerMovementManager.initializePossibleEndingSqures(FaceID);
    }
    //dice cheats
    public void ReadDiceOutcome(string s)
    {
        diceRes = s;
        Debug.Log(s);
        Debug.Log(diceRes);
    }
    public void startRolling()
    {
        Debug.Log("Button Works!");
        /*if (IsRollAllowed)
        {
            if (isRolling)
            {
                IsRollAllowed = false;
                PlayerMovementManager.initializePossibleEndingSqures(currentface);
            }
            // Toggle the dice rolling state
            isRolling = !isRolling;
        } */
    } 
}
