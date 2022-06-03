using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    // UI Array
    private GameObject[] UIs = new GameObject[6];
    
    [SerializeField]
    GameManager gameManager;

    void Start()
    {
        // get Game Manager
        gameManager = FindObjectOfType<GameManager>();

        //UIs[3] = GameObject.Find("04 ResutCanvas_Win");
        UIs[3] = transform.GetChild(0).gameObject;
        UIs[3].SetActive(false);
        // instantiate introUI
        //UIs[4] = GameObject.Find("05 ResutCanvas_Lost");
        UIs[4] = transform.GetChild(1).gameObject;
        UIs[4].SetActive(false);
        // instantiate introUI
        //UIs[5] = GameObject.Find("05 ResutCanvas_Lost");
        UIs[5] = transform.GetChild(1).gameObject;
        UIs[5].SetActive(false);
    }

    public void setIntroUI(GameObject gb){
        UIs[0] = gb;
        Debug.Log("intro Ui in UImanager: " + UIs[0]);
    }

    public void setPlayerHealthUI(GameObject gb){
        UIs[1] = gb;
        Debug.Log("PlayerHealth Ui in UImanager: " + UIs[1]);
        UIs[1].SendMessage("UpdateFill",160f);
    }

    public void setEnemyHealthUI(GameObject gb){
        UIs[2] = gb;
        Debug.Log("EnemyHealth Ui in UImanager: " + UIs[2]);
        UIs[2].SendMessage("UpdateFill",160f);
    }

    public void Update_02PlayerHealthUI(float PlayernewHealth){
        UIs[1].SendMessage("UpdateFill",PlayernewHealth);
    }
    
    public void Update_03EnemyHealthUI(float EnemynewHealth){
        UIs[2].SendMessage("UpdateFill",EnemynewHealth);
    }

    public void Show_04ResultUI_Win(){
        UIs[3].SetActive(true);
    }

    public void Show_05ResultUI_Lose(){
         UIs[4].SetActive(true);
    }

    public void Show_06ResultUI_Draw(){
         UIs[5].SetActive(true);
    }

    public void QuitBtnHandler(){
        gameManager.SendMessage("QuitGame");
    }

    public void RestartBtnHandler(){
       gameManager.SendMessage("RestartGame");
    }

}
