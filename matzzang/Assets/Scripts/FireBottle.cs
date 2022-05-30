using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBottle : MonoBehaviour
{
    public GameObject brokenObj;
    public Transform piovt;

    [SerializeField] bool isGrabing = false;
    //[SerializeField] float brokenFroce = 2f;
    [SerializeField] float flySpeed = 40f;

    private Vector3 flyRotation;
    private Vector3 flyRotationChild;

    private float angleRate = 0.5f;
    private float angleValueOrigin = 0.5f;

    private Rigidbody rb;
    private bool hasCollided = false;
    XRGrabInteractable xRGrabInteractable;

    //private float randomX;
    //private float randomY;
    //private float randomZ;
    private float randomSpeedrate;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        xRGrabInteractable = gameObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        RandomFlyRotation(flySpeed);
    }

    private void Update()
    {
        //print(rb.velocity.sqrMagnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.hasCollided == true) { return; }

        //if (rb.velocity.sqrMagnitude <= brokenFroce && collision.rigidbody.velocity.sqrMagnitude <= brokenFroce)
        //if (rb.velocity.sqrMagnitude <= brokenFroce || isGrabing == false)
        if (isGrabing == false)
        {
            return;

            //if (collision.gameObject.tag == "Destroyer")
            //{
            //    Instantiate(brokenObj, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            //    Destroy(this.gameObject);
            //}
        }
        else
        {
            this.hasCollided = true;
            //Instantiate(brokenObj, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            GameObject spawnedBroken = Instantiate(brokenObj, transform.position, transform.rotation);
            var chrb = spawnedBroken.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody item in chrb)
            {
                randomSpeedrate = Random.Range(0.7f, 0.9f);
                item.velocity = rb.velocity * randomSpeedrate;

                item.angularVelocity = RandomFlyRotation(flySpeed * randomSpeedrate) * 3f;
            }
            
            Destroy(this.gameObject);
            Destroy(spawnedBroken, 2f);
        }
    }

    void LateUpdate()
    {
        this.hasCollided = false;
    }

    public void FireRealBottle()
    {
        xRGrabInteractable.enabled = false;
        rb.velocity = flySpeed * piovt.forward;
        rb.angularVelocity = piovt.right * 50f + flyRotation;
        // rb.inertiaTensorRotation = Quaternion.Euler(50, 0, 0);
    }

    Vector3 RandomFlyRotation(float speed)
    {
        angleValueOrigin = speed * angleRate;

        flyRotation = new Vector3(Random.Range(-angleValueOrigin, angleValueOrigin), 
            Random.Range(-angleValueOrigin, angleValueOrigin), Random.Range(-angleValueOrigin, angleValueOrigin));

        return flyRotation;
    }

    public void IsGrabing()
    {
        isGrabing = true;
    }

    public void IsNotGrabing()
    {
        isGrabing = false;
    }
}
