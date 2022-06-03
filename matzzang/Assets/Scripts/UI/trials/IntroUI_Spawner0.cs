using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI_Spawner0 : MonoBehaviour
{
    public GameObject UIManager;

    private GameObject introUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(introUI == null){
            introUI = UIManager.transform.GetChild(0).gameObject;
            introUI.transform.SetParent(transform);
            introUI.transform.localPosition = Vector3.zero;
            Debug.Log("introUI in right position: "+ introUI);
        }
    }
}
