using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimV3Line_v1 : MonoBehaviour
{
    [Header("Aim")]
    public GameObject[] bulletPrefabs;
    public Transform target;
    public float targetYOffset = 0;
    public Transform point;

    private float YthanTarget = 2f;
    [SerializeField, Range(0.5f, 5f)] float YthanTargetStandValue;
    [SerializeField, Range(0f, 2.5f)] float YthanTargetOffset = 0;

    public float standIntervalTime = 1f;
    [SerializeField, Range(0f, 0.9f)] float IntervalTimeOffsetNegative = 0f;
    [SerializeField, Range(0f, 0.9f)] float IntervalTimeOffsetPositive = 0f;

    // line
    [Header("Prediction")]
    public int gravityScale = 1;
    public int predictionCount = 10;
    public GameObject predictionPrefabs;
    public bool isDisplayLine = true;

    // Data
    float Gravity;
    Vector3 velocity;

    [Header("SlowMotion")]
    public bool isSlowMotion = true;
    public float timeZoomRate = 0.5F;
    bool isStopMove;

    //void Start()
    //{
    //    InvokeRepeating(nameof(Shoot), 0, interval);
    //}

    private void Update()
    {
        //gameObject.SendMessage("IsStopMove", isStopMove);

        if (isSlowMotion)
        {
            if (Time.timeScale != timeZoomRate)
                Time.timeScale = timeZoomRate;
        }

        StartCoroutine(nameof(FireInterval));
    }

    IEnumerator FireInterval()
    {
        yield return new WaitForSeconds(Random.Range(standIntervalTime * (1- IntervalTimeOffsetNegative), standIntervalTime * (1 + IntervalTimeOffsetPositive)));
        
        //isStopMove = true;
        //yield return new WaitForSeconds(0.01f);

        Shoot();

        //yield return new WaitForSeconds(0.01f);
        //isStopMove = false;

        StopCoroutine(nameof(FireInterval));

        
    }

    void Shoot()
    {
        YthanTarget = Random.Range(YthanTargetStandValue - YthanTargetOffset, YthanTargetStandValue + YthanTargetOffset);
        YthanTarget = Mathf.Clamp(YthanTarget, 0.5f, 5f);

        CalculateData();

        // if (isDisplayLine)
        // {
        //     // Pre-judgment
        //     //Instantiate(predictionPrefabs);
        //     Vector3 predictionVelocity = velocity;
        //     Vector3 predictionPosition = point.position;
        //     float predictionInterval = Mathf.Abs(point.position.x - target.position.x) / Mathf.Abs(velocity.x) / predictionCount;
        //     for (int i = 0; i < predictionCount; i++)
        //     {
        //         predictionPosition += predictionVelocity * predictionInterval + new Vector3(0, 0.5f * -Gravity * predictionInterval * predictionInterval);  // Move point
        //         predictionVelocity += new Vector3(0, -Gravity * predictionInterval);  // gravity
        //         GameObject line = Instantiate(predictionPrefabs, predictionPosition, Quaternion.identity, transform);
        //         Destroy(line, standIntervalTime);
        //     }
        // }

        // Fire
        point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

        int a = Random.Range(0, bulletPrefabs.Length);
        GameObject bullet = Instantiate(bulletPrefabs[a], point.position, Quaternion.identity);
        //bullet.GetComponent<EnemyBullet3Bottle>().Shoot(velocity);
        //bullet.GetComponent<Rigidbody2D>().velocity = velocity;
        bullet.SendMessage("Shoot", velocity);
        bullet.SendMessage("FiredByOtherRotation", (transform.position - velocity).sqrMagnitude);
    }

    void CalculateData()
    {
        Gravity = Mathf.Abs(Physics.gravity.y * gravityScale);

        //float Gravity = Mathf.Abs(Physics.gravity.y * bulletPrefab.GetComponent<Rigidbody>().mass);
        float height_1, height_2;
        if (target.position.y > transform.position.y)
        {
            height_1 = YthanTarget + target.position.y + targetYOffset - transform.position.y;
            height_2 = YthanTarget;
        }
        else
        {
            height_1 = YthanTarget;
            height_2 = transform.position.y + YthanTarget - target.position.y;
        }
        float time_1 = Mathf.Sqrt(2 * height_1 / Gravity);
        float time_2 = Mathf.Sqrt(2 * height_1 / Gravity) + Mathf.Sqrt(2 * height_2 / Gravity);
        Vector3 distance = target.position - transform.position;
        distance.y = 0;
        Vector3 speed = distance / time_2;
        velocity = speed + time_1 * Gravity * Vector3.up;
    }


    //private void OnEnable()
    //{
    //    if (isSlowMotion)
    //    {
    //        Time.timeScale = timeZoomRate;
    //    }
    //}

    private void OnDisable()
    {
        if (Time.timeScale != 1F)
        {
            Time.timeScale = 1F;
        }
    }
}
