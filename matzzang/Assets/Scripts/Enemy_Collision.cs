using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision : MonoBehaviour
{
    public GameManager gameManager;
    public float EnemyHealthLoss = 20f;
    //[SerializeField] AudioData hitAudio;

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Bottle")){
            gameManager.SendMessage("UpdateEnemyHealth",EnemyHealthLoss);
            //AudioManager.Instance.PlayRandomSFX(hitAudio);
        }
    }
}
