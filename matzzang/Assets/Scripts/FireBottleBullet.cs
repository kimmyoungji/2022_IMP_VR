using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBottleBullet : MonoBehaviour
{
    public bool IsGrabing { get; set; } = false;
    public GameObject brokenObj;
    public Transform piovt;

    //[SerializeField] float brokenFroce = 2f;
    [SerializeField] float flySpeed = 40f;
    [SerializeField] AudioClip brokenSound;

    private Vector3 flyRotation;
    //private Vector3 flyRotationChild;

    private float angleRate = 0.5f;
    private float angleValueOrigin = 0.5f;

    private Rigidbody rb;
    private bool hasCollided = false;
    XRGrabInteractable xRGrabInteractable;

    //private float randomX;
    //private float randomY;
    //private float randomZ;
    private float randomSpeedrate;

    // line
    [Header("Prediction")]
    public int gravityScale = 1;
    public int predictionCount = 10;
    public int displayInterval = 10;
    public GameObject predictionPrefabs;
    public bool isDisplayLine = true;

    // Data
    float Gravity;
    //Vector3 velocity;


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
        if (IsGrabing && isDisplayLine)
        {
            Gravity = Mathf.Abs(Physics.gravity.y * gravityScale);

            // Pre-judgment
            Vector3 predictionVelocity = flySpeed * piovt.forward;
            Vector3 predicrionPosition = piovt.position;

            float predictionInterval = displayInterval / Mathf.Abs(predictionVelocity.x) / predictionCount;
            //float predictionInterval = Mathf.Abs(firePoint.position.x - Vector3.up.x) / Mathf.Abs(velocity.x) / predictionCount;

            for (int i = 0; i < predictionCount; i++)
            {
                predicrionPosition += predictionVelocity * predictionInterval + new Vector3(0, 0.5f * -Gravity * predictionInterval * predictionInterval);  // Move point
                predictionVelocity += new Vector3(0, -Gravity * predictionInterval);  // gravity
                GameObject line = Instantiate(predictionPrefabs, predicrionPosition, Quaternion.identity, transform);


                Destroy(line, 0.02f);
                //if (!isGrabing)
                //{
                //    Destroy(line);
                //}
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.hasCollided == true) { return; }

        if (IsGrabing == false && !collision.transform.CompareTag("Enemy"))
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

            AudioSource.PlayClipAtPoint(brokenSound, transform.position, 0.3f);
            Destroy(this.gameObject);
            Destroy(spawnedBroken, 2f);
        }
        else
        {
            return;
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

    public void FiredByOtherRotation(float speed)
    {
        RandomFlyRotation(speed);
        rb.angularVelocity = piovt.right * 50f + flyRotation;
    }
}
