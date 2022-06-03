using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.XR.CoreUtils;


// our own implementation of continuous movement in VR
public class CustomContinuousMovement_v1 : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private InputDeviceCharacteristics devChars;

    private InputDevice targetDevice;

    // for storing user input
    private Vector2 inputAxis;

    private CharacterController characterController;

    // get XR origin to use the camera
    private XROrigin origin;

    //  gravity
    public float gravity = -9.81f;

    // current falling speed
    private float fallingSpeed;

    // space above the eyes for calculating total height
    public float additionalHeight = 0.2f;


    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        origin = GetComponent<XROrigin>();

        TryInitialize();

    }

    // try to find the XR input device based on the device characteristics.
    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(devChars, devices);

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }


    // Update is called once per frame
    void Update()
    {
        // try to initialize again if needed
        if(targetDevice == null || !targetDevice.isValid)
        {
            TryInitialize();
            return;
        }

        // are we on the ground?
        bool isGrounded = characterController.isGrounded;

        if(isGrounded && fallingSpeed < 0)
        {
            fallingSpeed = 0; // we are on the ground,so let's not fall anymore.
        }



        // move the player based on the controller input (joystick)
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        // get rotation around y-axis based on the camera's y Euler angle
        Quaternion headYaw = Quaternion.Euler(0, origin.Camera.transform.eulerAngles.y, 0);
        
        // set the movement direction based on input.
        // multiply the direction by the amount of y-rotation of the head
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // joystick movement
        characterController.Move(direction * Time.deltaTime * speed);

        // make character capsule follow the headset
        CapsuleFollowHeadset();

        // gravity movement (downward)

        // increase falling speed per frame (only when falling)
        if(!isGrounded)
            fallingSpeed += gravity * Time.deltaTime;

        // move by gravity
        characterController.Move(Vector3.up * fallingSpeed * Time.deltaTime);

    }

    // make body capsule follow the camera

    private void CapsuleFollowHeadset()
    {
        // adjust the character controller height based on the user's heigth
        characterController.height = origin.CameraInOriginSpaceHeight + additionalHeight;

        // get the center of the capsule in the head by transforming from world to local position
        Vector3 capsuleCenter = transform.InverseTransformPoint(origin.Camera.transform.position);

        // set new character center according to the camera position
        characterController.center = new Vector3(capsuleCenter.x,
            characterController.height / 2 + characterController.skinWidth,
            capsuleCenter.z);

    }
}
