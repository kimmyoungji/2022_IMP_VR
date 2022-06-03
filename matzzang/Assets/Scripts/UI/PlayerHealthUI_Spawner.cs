using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI_Spawner : MonoBehaviour
{
    [SerializeField]
    private UIManager UIManager;

    [SerializeField]
    private GameObject PlayerHealthUIprefab;
    private GameObject PlayerHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        PlayerHealthUI = Instantiate(PlayerHealthUIprefab,transform);
        UIManager.SendMessage("setPlayerHealthUI",PlayerHealthUI);
    }

}
