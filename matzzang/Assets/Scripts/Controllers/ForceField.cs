using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float addForceMultiplier = 5f;
    public BoxingController boxingController;
    public Transform rotationTarget;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 realVelocity = Quaternion.AngleAxis(rotationTarget.rotation.eulerAngles.y,Vector3.up) * boxingController.controllerVelocity;

        collision.transform.GetComponent<Rigidbody>().AddForce(realVelocity * addForceMultiplier, ForceMode.Impulse);
        boxingController.SendMessage("GetHaptic");
    }
}
