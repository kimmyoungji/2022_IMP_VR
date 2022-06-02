using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // health 0~160
    public float EnemeyHealth = 160; // Enemy ealth: min 0 ~ max 160
    public float PlayerHealth = 160; // Player Health: min 0 ~ max 160
    private bool isHealthUpdated;

    // game result
    private enum GameResult { win, lose, draw, proceeding}
    private GameResult gameResult;

    public GameObject UIManager;
    public GameObject AudioManager;
    public ControllerManager controllerManager;
    
    void Start(){}

    public void Update()
    {
            // compare Player's and Enemy's Health
            if(PlayerHealth > 0 && EnemeyHealth <= 0)
            {
                gameResult = GameResult.win;
                UIManager.SendMessage("Show_04ResultUI_Win");
                controllerManager.SendMessage("setHandRayActive",true);
            }
            else if(PlayerHealth <= 0 && EnemeyHealth > 0)
            {
                gameResult = GameResult.lose;
                UIManager.SendMessage("Show_05ResultUI_Lose");
                controllerManager.SendMessage("setHandRayActive",true);
            }
            else if(PlayerHealth <= 0 && EnemeyHealth <= 0){
                gameResult = GameResult.draw;
                UIManager.SendMessage("Show_06ResultUI_Draw");
                controllerManager.SendMessage("setHandRayActive",true);
            }
            else
                gameResult = GameResult.proceeding;    
    }


    public void UpdatePlayerHealth( float PHealthLoss){
        PlayerHealth -= PHealthLoss;
        Debug.Log("Enemy Health: "+ EnemeyHealth + " , Player Health: " + PlayerHealth );
        UIManager.SendMessage("Update_02PlayerHealthUI",PlayerHealth);
    }

    public void UpdateEnemyHealth( float EHealthLoss){
        EnemeyHealth -= EHealthLoss;
        Debug.Log("Enemy Health: "+ EnemeyHealth + " , Player Health: " + PlayerHealth );
        UIManager.SendMessage("Update_03EnemyHealthUI",EnemeyHealth);
    }

    public void QuitGame(){
        // save any game data here
         #if UNITY_EDITOR
             // Application.Quit() does not work in the editor so
             // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
             UnityEditor.EditorApplication.isPlaying = false;
         #else
             Application.Quit();
         #endif
    }

    public void RestartGame(){
        SceneManager.LoadScene("01 LevelSelectionScene",LoadSceneMode.Single);
    }

}
