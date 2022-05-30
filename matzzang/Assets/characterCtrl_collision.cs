using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterCtrl_collision : MonoBehaviour
{   
    public GameObject GameManager;
    public float PlayerHealthLoss = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.CompareTag("Bottle")){
            GameManager.SendMessage("UpdatePlayerHealth",PlayerHealthLoss);
        }
    }

    
}
