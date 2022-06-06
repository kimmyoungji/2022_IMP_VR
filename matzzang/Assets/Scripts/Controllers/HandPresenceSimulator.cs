using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class HandPresenceSimulator : MonoBehaviour
{


    [SerializeField]
    private GameObject handModelPrefab;

    private GameObject spawnedHand;

    private Animator handAnimator;  

    private float gPressed; 
    private float bPressed;


    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();  

    }

    private void TryInitialize()
    {
        if(spawnedHand == null) {
            // spawn the hand
            spawnedHand = Instantiate(handModelPrefab, transform);

            // get an animator
            handAnimator = spawnedHand.GetComponent<Animator>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {       
        spawnedHand.SetActive(true); 
        UpdateHandAnimation();
        gPressed = Keyboard.current.gKey.ReadValue(); 
        bPressed = Keyboard.current.bKey.ReadValue();
    }

    private void UpdateHandAnimation( )
    {
        // trigger pressed
        if(bPressed>0)
        {
            handAnimator.SetFloat("Trigger", bPressed);
        } else {
            handAnimator.SetFloat("Trigger",0);
        }

        // grip pressed
        if (gPressed>0)
        {
            handAnimator.SetFloat("Grip", gPressed);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }


    }

    

}
