using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI_Spawner : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject introUIprefab;
    private GameObject introUI;

    // Start is called before the first frame update
    void Start()
    {
        introUI = Instantiate(introUIprefab,transform);
        UIManager.SendMessage("setIntroUI",introUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
