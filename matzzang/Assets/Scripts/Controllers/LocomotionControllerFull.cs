using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionControllerFull : MonoBehaviour
{
    // Telport
    //public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    // switch Turn & Telport
    public XRNode inputSource;
    private Vector2 inputAxis;
    private Vector2 standVector;

    //public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    // CharacterController - stepOffset
    public float maxStepOffset = 1.0f;
    private float originStepOffsetValue;

    private void Start()
    {
        standVector = new Vector2(1, 0);
        originStepOffsetValue = gameObject.GetComponent<CharacterController>().stepOffset;
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        float angle = DotToAngle(standVector, inputAxis);
        if (angle >= 50 && angle <= 130)
        {
            gameObject.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;

            //if (leftTeleportRay)
            //{
            //    leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActived(leftTeleportRay));
            //}

            if (rightTeleportRay)
            {
                rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActived(rightTeleportRay));
            }
        }
        else if (rightTeleportRay.gameObject.activeInHierarchy == false && inputAxis.magnitude >= activationThreshold)
        {
            gameObject.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
        }

        if (angle >= 50 && angle <= 130 && rightTeleportRay.gameObject.activeInHierarchy == false && inputAxis.magnitude >= activationThreshold)
        {
            gameObject.GetComponent<CharacterController>().stepOffset = maxStepOffset;
        }
        else
        {
            gameObject.GetComponent<CharacterController>().stepOffset = originStepOffsetValue;
        }
    }

    public bool CheckIfActived(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice,teleportActivationButton,out bool isActived, activationThreshold);
        return isActived;
    }

    public float DotToAngle(Vector3 _from, Vector3 _to)
    {
        float rad = Mathf.Acos(Vector3.Dot(_from.normalized, _to.normalized));
        return rad * Mathf.Rad2Deg;
    }
}
