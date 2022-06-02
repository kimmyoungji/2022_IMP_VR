using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCtrl_collision : MonoBehaviour
{   
    public GameObject GameManager;
    public float PlayerHealthLoss = 20f;

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     if(hit.gameObject.CompareTag("Bottle")){
    //         Debug.Log("Player collied with a Bottle");
    //         GameManager.SendMessage("UpdatePlayerHealth",PlayerHealthLoss);
    //     }
    // }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Bottle")){
            //Debug.Log("Player collied with a Bottle");
            GameManager.SendMessage("UpdatePlayerHealth",PlayerHealthLoss);
        }
    }
}
