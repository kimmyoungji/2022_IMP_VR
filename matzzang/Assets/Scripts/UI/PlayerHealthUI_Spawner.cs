using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI_Spawner : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject PlayerHealthUIprefab;
    private GameObject PlayerHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthUI = Instantiate(PlayerHealthUIprefab,transform);
        //Debug.Log("PlayerHealthUI From UI Spawner: "+ PlayerHealthUI);
        UIManager.SendMessage("setPlayerHealthUI",PlayerHealthUI);
    }

}
