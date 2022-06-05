using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl_collision : MonoBehaviour
{   
    [SerializeField]
    GameManager GameManager;
    public float PlayerHealthLoss = 20f;

    private void Start() {
        GameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Bottle")){
            //Debug.Log("Player collied with a Bottle");
            GameManager.SendMessage("UpdatePlayerHealth",PlayerHealthLoss);
        }
    }
}
