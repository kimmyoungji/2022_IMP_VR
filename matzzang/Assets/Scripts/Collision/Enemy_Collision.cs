using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision : MonoBehaviour
{
    [SerializeField]
    GameManager GameManager;
    public float EnemyHealthLoss = 20f;
    //[SerializeField] AudioData hitAudio;

    private void Start() {
        GameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.CompareTag("Bottle"))
        {
            GameManager.SendMessage("UpdateEnemyHealth", EnemyHealthLoss);
            //AudioManager.Instance.PlayRandomSFX(hitAudio);
        }
        else if (other.collider.CompareTag("BoxingFragment"))
        {
            GameManager.SendMessage("UpdateEnemyHealth", EnemyHealthLoss * 0.25);
        }
    }
}
