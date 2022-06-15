using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

using Unity.XR.CoreUtils;

public class CustomContinuousMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    // Get slow motin value
    public EnemyAimV3Line enemyAimV3Line;
    public ActionBasedSnapTurnProvider actionBasedSnapTurnProvider;
    private float slowMotionValue = 1F;
    private float originSpeed = 1.5f;
    private float originTurnInterval = 0.2f;

    private float fallingSpeed;
    private XROrigin origin;
    private Vector2 inputAxis;
    private CharacterController character;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        origin = GetComponent<XROrigin>();
    }

    void Start()
    {
        originSpeed = speed;
        originTurnInterval = actionBasedSnapTurnProvider.debounceTime;
        GetNewSlowMotionValue();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        GetNewSlowMotionValue();
    }

    private void FixedUpdate()
    {
        CapsuleFllowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, origin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        // gravity
        bool isGrounded = CheckIfGrounded();
        if(isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(fallingSpeed * Time.fixedDeltaTime * Vector3.up);
    }

    void CapsuleFllowHeadset()
    {
        character.height = origin.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(origin.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    public bool CheckIfGrounded()
    {
        // tell us if on ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    private void GetNewSlowMotionValue()
    {
        if (enemyAimV3Line)
        {
            if (enemyAimV3Line.isSlowMotion)
            {
                if (slowMotionValue != enemyAimV3Line.timeZoomRate)
                {
                    slowMotionValue = enemyAimV3Line.timeZoomRate;
                    speed = originSpeed / slowMotionValue;
                    actionBasedSnapTurnProvider.debounceTime = originTurnInterval * slowMotionValue;
                }
            }
            else
            {
                speed = originSpeed;
                actionBasedSnapTurnProvider.debounceTime = originTurnInterval;
            }
        }
        else
        {
            speed = originSpeed;
            actionBasedSnapTurnProvider.debounceTime = originTurnInterval;
        }
    }
}
