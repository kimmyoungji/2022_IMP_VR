using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // health 0~180
    public float EnemeyHealth = 180; // Enemy ealth: min 0 ~ max 180
    public float PlayerHealth = 180; // Player Health: min 0 ~ max 180

    // Ui objects
    private GameObject[] UIobject = new GameObject[5];
    private bool isHealthUpdated = true;

    // game result
    private enum GameResult { win, lost, draw, proceeding}
    private GameResult gameResult;

    public GameObject UIManager;
    public GameObject AudioManager;
    
    void Start()
    {

    }

    void Update()
    {
        if(isHealthUpdated)
        {
            // compare Player's and Enemy's Health
            if(PlayerHealth > 0 && EnemeyHealth <= 0)
            {
                gameResult = GameResult.win;
                Time.timeScale = 0;
                UIManager.SendMessage("Show_04ResultUI_Win");
            }
            else if(PlayerHealth <= 0 && EnemeyHealth > 0)
            {
                gameResult = GameResult.lost;
                Time.timeScale = 0;
                UIManager.SendMessage("Show_05ResultUI_Lost");
            }
            else if(PlayerHealth <= 0 && EnemeyHealth <= 0)
                gameResult = GameResult.draw;
            else
                gameResult = GameResult.proceeding;

            // print result
            Debug.Log(gameResult);
            Debug.Log("Enemy Health: "+ EnemeyHealth + " , Player Health: " + PlayerHealth );
            //isHealthUpdated = false;
        }
    }


    void UpdatePlayerHealth( int PHealthLoss){
        PlayerHealth -= PHealthLoss;
        UIManager.SendMessage("Update_02PlayerHealthUI",PlayerHealth);
        //isHealthUpdated = true;
    }

     void UpdateEnemyHealth( int EHealthLoss){
        EnemeyHealth -= EHealthLoss;
        UIManager.SendMessage("Update_03EnemyHealthUI",EnemeyHealth);
        //isHealthUpdated = true;
    }

}
