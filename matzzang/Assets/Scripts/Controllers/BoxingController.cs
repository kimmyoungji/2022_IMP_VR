using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoxingController : MonoBehaviour
{

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    public Vector3 controllerVelocity;
    UnityEngine.XR.HapticCapabilities capabilities;

    [SerializeField] float hapticAmplitude = 1f;
    [SerializeField] float hapticDuration = 0.1f;

    //public bool IsGrabingBoxing { get; set; } = false;
    [SerializeField] float targetSpeed = 5f;
    [SerializeField] GameObject forceField;
    [SerializeField] float forceFieldContinueTime = 0.2f;

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
            targetDevice.TryGetHapticCapabilities(out capabilities);

            controllerVelocity = deviceVelocityValue;

            //var speed = deviceVelocityValue.sqrMagnitude;
            var speed = deviceVelocityValue.sqrMagnitude;

            if (gripButtonPressed && speed >= targetSpeed)
            {
                isForceFieldOn = true;
                forceField.SetActive(true);
            }
            else
            {
                if (isForceFieldOn)
                    StartCoroutine(ForceFieldContinueTime());
            }
        }

        IEnumerator ForceFieldContinueTime()
        {
            yield return new WaitForSecondsRealtime(forceFieldContinueTime);  // WaitForSecondsRealtime will not get effect by slow motion. And 0.1 is good.
            isForceFieldOn = false;
            forceField.SetActive(false);
        }
    }

    public void GetHaptic()
    {
        uint channel = 0;
        targetDevice.SendHapticImpulse(channel, hapticAmplitude, hapticDuration);
    }
}
