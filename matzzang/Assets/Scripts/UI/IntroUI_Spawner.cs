using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI_Spawner : MonoBehaviour
{
    [SerializeField]
    private UIManager UIManager;

    [SerializeField]
    private GameObject introUIprefab;
    private GameObject introUI;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        introUI = Instantiate(introUIprefab,transform);
        UIManager.SendMessage("setIntroUI",introUI);
    }
}
