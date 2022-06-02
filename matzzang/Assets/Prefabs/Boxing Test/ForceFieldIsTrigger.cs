using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldIsTrigger : MonoBehaviour
{
    public float addForceMultiplier = 2f;
    public BoxingController boxingController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Vector3 realVelocity = Quaternion.AngleAxis(gameObject.transform.parent.parent.rotation.eulerAngles.y, Vector3.up) * boxingController.controllerVelocity;

            other.transform.GetComponent<Rigidbody>().AddForce(realVelocity * addForceMultiplier, ForceMode.Impulse);
            other.transform.SendMessage("HitResponseOther");
        }
    }
}
