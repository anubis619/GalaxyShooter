using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameIsOn = false;
    public GameObject PlayerObject;

    private UIManager uiMananger;

    private void Start()
    {
        uiMananger = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    private void Update()
    {
        if(GameIsOn == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(PlayerObject, Vector3.zero, Quaternion.identity);
                GameIsOn = true;
                uiMananger.HideTitleScreen();
            }
        } 
    }
}
