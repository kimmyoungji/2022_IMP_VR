using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Animator_controller : MonoBehaviour
{
    public Animator animator;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if( other.CompareTag("Player") ){
            animator.SetBool("isCollied3",true);
            gameManager.SendMessage("LoadLevel3");
        }
    }
}
