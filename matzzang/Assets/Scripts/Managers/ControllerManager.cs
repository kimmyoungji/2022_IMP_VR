using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject LeftHandCtrller;
    public GameObject RightHandCtrller;
    public GameObject RightHandRay;
    public GameObject LeftHandRay;

    // Start is called before the first frame update
    void Start()
    {
        LeftHandCtrller = GameObject.Find("LeftHand Controller");
        RightHandCtrller = GameObject.Find("RightHand Controller");
        RightHandRay = GameObject.Find("RigihtHand Ray");
        RightHandRay.SetActive(false);
        LeftHandRay = GameObject.Find("LeftHand Ray");
        LeftHandRay.SetActive(false);

        Debug.Log("LeftHandCtrller :"+ LeftHandCtrller);
        Debug.Log("RightHandCtrller :"+ RightHandCtrller);
        Debug.Log("RightHandRay :"+ RightHandRay);
        Debug.Log("LeftHandRay :"+ LeftHandRay);
    }

    public void setControllerActive(bool isCtrllerActive){
        LeftHandCtrller.SetActive(isCtrllerActive);
        RightHandCtrller.SetActive(isCtrllerActive);
    }

    public void setHandRayActive(bool isHandRayActive){
        RightHandRay.SetActive(isHandRayActive);
        LeftHandRay.SetActive(isHandRayActive);
    }
}
