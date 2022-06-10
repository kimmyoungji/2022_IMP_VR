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

    //[SerializeField] private UIManager UIManager;
    [SerializeField] GameObject pauseUI;
    bool isPressed = false;
    //bool interrupt = false;

    void Start()
    {
        TryInitialize();
        //UIManager = FindObjectOfType<UIManager>();
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
            targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonPressed);
            targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 deviceVelocityValue);
            targetDevice.TryGetHapticCapabilities(out capabilities);

            controllerVelocity = deviceVelocityValue;

            //var speed = deviceVelocityValue.sqrMagnitude;
            var speed = deviceVelocityValue.sqrMagnitude;

            isPressed = menuButtonPressed;

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

        Paused();

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

    public void Paused()
    {
        if (pauseUI)
        {
            if (isPressed && !pauseUI.activeInHierarchy)
            {
                pauseUI.SetActive(true);
            }
        }
        
        //if (pauseUI)
        //{
        //    if (isPressed && !interrupt && !pauseUI.activeInHierarchy)
        //    {
        //        pauseUI.SetActive(true);
        //    }
        //    else if (!isPressed && !interrupt && pauseUI.activeInHierarchy)
        //    {
        //        interrupt = true;
        //    }
        //    else if (isPressed && interrupt && pauseUI.activeInHierarchy)
        //    {
        //        pauseUI.SetActive(false);
        //    }
        //    else if (!isPressed && interrupt && !pauseUI.activeInHierarchy)
        //    {
        //        interrupt = false;
        //    }
        //}
    }
}
