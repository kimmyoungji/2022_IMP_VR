using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_v1 : MonoBehaviour
{
    private GameObject[] HealthUIobject = new GameObject[2];
    private GameObject[] ResultUIobject = new GameObject[2];
    
    void Start()
    {
       // get UI objects in scene
       HealthUIobject = GameObject.FindGameObjectsWithTag("HealthUI");
       Update_02PlayerHealthUI(180);
       Update_03EnemyHealthUI(180);
       ResultUIobject = GameObject.FindGameObjectsWithTag("ResultUI");

        // print ui objects to check
        // Health ui
       foreach(GameObject ui in HealthUIobject)
       {
           Debug.Log(ui);
       }
       // result ui
       foreach(GameObject ui in ResultUIobject)
       {
           Debug.Log(ui);
           ui.SetActive(false);
       }
    }


    public void Update_02PlayerHealthUI(int PlayernewHealth){
        RectTransform fill = HealthUIobject[1].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Debug.Log("Update_02PlayerHealthUI: " + fill);
        fill.sizeDelta = new Vector2(PlayernewHealth,10);
    }
    
    public void Update_03EnemyHealthUI(int EnemynewHealth){
        RectTransform fill = HealthUIobject[0].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Debug.Log("Update_03EnemyHealthUI: " + fill);
        fill.sizeDelta = new Vector2(EnemynewHealth,10);
    }

    public void Show_04ResultUI_Win(){
        ResultUIobject[0].SetActive(true);
    }

    public void Show_05ResultUI_Lost(){
         ResultUIobject[1].SetActive(true);
    }
}
