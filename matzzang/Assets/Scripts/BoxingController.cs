using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoxingController : MonoBehaviour
{
    public Vector3 controllerVelocity;
    public Quaternion controllerRotation;

    //public bool IsGrabingBoxing { get; set; } = false;
    [SerializeField] float targetSpeed = 5f;
    [SerializeField] GameObject forceField;
    [SerializeField] float forceFieldContinueTime = 0.2f;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    bool isForceFieldOn = false;

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()  // ≥¢ ‘≥ı ºªØ
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + "Boxing controller ready");
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    private void FixedUpdate()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonPressed);
            targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 deviceVelocityValue);
            targetDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotationValue);

            controllerVelocity = deviceVelocityValue;
            controllerRotation = deviceRotationValue;

            var speed = deviceVelocityValue.sqrMagnitude;

            if (gripButtonPressed && speed >= targetSpeed)
            {
                isForceFieldOn = true;
                forceField.SetActive(true);
            }
            else
            {
                if(isForceFieldOn)
                    StartCoroutine(ForceFieldContinueTime());
            }
        }

        IEnumerator ForceFieldContinueTime()
        {
            yield return new WaitForSeconds(forceFieldContinueTime);
            isForceFieldOn = false;
            forceField.SetActive(false);
        }
    }
}
