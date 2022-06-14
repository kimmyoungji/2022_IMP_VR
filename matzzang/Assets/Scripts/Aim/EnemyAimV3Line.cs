using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimV3Line : MonoBehaviour
{
    // for animming
    [Header("Aim")]
    public GameObject[] bulletPrefabs;
    public GameObject[] boxingPrefabs;
    public GameObject[] realThrowPrefabs;
    public Transform target;
    public Transform point;

    //public float enemyGrabYOffset = -0.16f;
    private float enemyGrabYOffset = -0.16f;


    //public float targetYOffset = 0;
    private float targetYOffset = 0;

    private GameObject bullet;

    int prefabsTypeNum = 0;
    private ArrayList PrefabList = new ArrayList(5);

    private float YthanTarget = 2f;
    [SerializeField, Range(0.1f, 0.75f)] float YthanTargetStandValue;
    [SerializeField, Range(0f, 2.5f)] float YthanTargetOffset = 0;


    // shooting interval time
    public float standIntervalTime = 1f;
    [SerializeField, Range(0f, 0.9f)] float IntervalTimeOffsetNegative = 0f;
    [SerializeField, Range(0f, 0.9f)] float IntervalTimeOffsetPositive = 0f;

    private float realIntervalTime;
    private float WaitForAnimationTime = 0.24f;
    private float randomTime;

    [Header("RandomSystem")]
    // control the ratio of bottle type
    [SerializeField, Range(1f, 10f)] int randomrange;
    [SerializeField, Range(1f, 10f)] int randomrangeLeft;  // Bigger & equal than
    [SerializeField, Range(1f, 10f)] int randomrangeRight;  // smaller than

    // Max fire number of bottles in one colling;
    [SerializeField] float intervalInGroup = 0.5f;
    [SerializeField, Range(1f, 5f)] int type1MaxNum;
    [SerializeField, Range(1f, 5f)] int type2MaxNum;
    [SerializeField, Range(1f, 5f)] int type3MaxNum;

    private bool isStart;
    private GameObject[] loopGroup;
    private int loopTimes = 0;

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
    //bool isStopMove;

    // Fix low frame rate issue in slow motion.
    float defultFixedDeltaTime;

    private Animator animator;

    public bool isPaused = false;
    //float defultTimeZoomRate;


    void Awake()
    {
        defultFixedDeltaTime = Time.fixedDeltaTime;
        //defultTimeZoomRate = timeZoomRate;
        targetYOffset -= enemyGrabYOffset;
        animator = gameObject.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;  // Make animation playback unaffected by time scaling.
    }

    void Start()
    {
        if (bulletPrefabs.Length > 0)
        {
            PrefabList.Add(bulletPrefabs);
            prefabsTypeNum++;
        }
        if (boxingPrefabs.Length > 0)
        {
            PrefabList.Add(boxingPrefabs);
            prefabsTypeNum++;
        }
        if (realThrowPrefabs.Length > 0)
        {
            PrefabList.Add(realThrowPrefabs);
            prefabsTypeNum++;
        }

        FirePreloadJudgment();
        realIntervalTime = Random.Range(standIntervalTime * (1 - IntervalTimeOffsetNegative), standIntervalTime * (1 + IntervalTimeOffsetPositive)) * timeZoomRate;
    }

    private void Update()
    {
        SlowMotion();

        if(!isPaused)
            StartCoroutine(nameof(FireInterval));

        if (bullet)
        {
            var grabPos = new Vector3(point.position.x, point.position.y + enemyGrabYOffset, point.position.z);
            bullet.transform.position = grabPos;
            bullet.transform.rotation = transform.rotation;
        }
    }

    IEnumerator FireInterval()
    {
        if (loopTimes <= 0)
        {
            realIntervalTime = Random.Range(standIntervalTime * (1 - IntervalTimeOffsetNegative), standIntervalTime * (1 + IntervalTimeOffsetPositive)) * timeZoomRate;
        }

        yield return new WaitForSeconds(realIntervalTime);

        if (loopTimes > 0)
        {
            realIntervalTime = intervalInGroup * timeZoomRate;
        }

        //Shoot();
        animator.SetBool("Throw",true);
        StartCoroutine(WaitForAnimation());

        StopCoroutine(nameof(FireInterval));
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(WaitForAnimationTime);

        if (bullet)
        {
            Shoot();
        }
        StartCoroutine(SetThrowFalse());
    }

    IEnumerator SetThrowFalse()
    {
        yield return new WaitForSeconds(randomTime*0.5f);
        animator.SetBool("Throw",false);

        if(loopTimes <= 0)
        {
            FirePreloadJudgment();
        }
        else
        {
            Loop();
        }
    }

    void Shoot()
    {
        YthanTarget = Random.Range(YthanTargetStandValue - YthanTargetOffset, YthanTargetStandValue + YthanTargetOffset);
        YthanTarget = Mathf.Clamp(YthanTarget, 0.1f, 3f);

        CalculateDate();

        if (isDisplayLine)
        {
            // Pre-judgment
            //Instantiate(predictionPrefabs);
            Vector3 predictionVelocity = velocity;
            Vector3 predicrionPosition = point.position;
            float predictionInterval = Mathf.Abs(point.position.x - target.position.x) / Mathf.Abs(velocity.x) / predictionCount;
            for (int i = 0; i < predictionCount; i++)
            {
                predicrionPosition += predictionVelocity * predictionInterval + new Vector3(0, 0.5f * -Gravity * predictionInterval * predictionInterval);  // Move point
                predictionVelocity += new Vector3(0, -Gravity * predictionInterval);  // gravity
                GameObject line = Instantiate(predictionPrefabs, predicrionPosition, Quaternion.identity);
                Destroy(line, standIntervalTime);
            }
        }

        Fire();
    }

    void CalculateDate()
    {
        Gravity = Mathf.Abs(Physics.gravity.y * gravityScale);

        //float Gravity = Mathf.Abs(Physics.gravity.y * bulletPrefab.GetComponent<Rigidbody>().mass);
        float height_1, height_2;
        if (target.position.y > point.position.y)
        {
            height_1 = YthanTarget + target.position.y + targetYOffset - point.position.y;
            height_2 = YthanTarget;
        }
        else
        {
            height_1 = YthanTarget;
            height_2 = point.position.y + YthanTarget - target.position.y;
        }
        float time_1 = Mathf.Sqrt(2 * height_1 / Gravity);
        float time_2 = Mathf.Sqrt(2 * height_1 / Gravity) + Mathf.Sqrt(2 * height_2 / Gravity);
        Vector3 distance = target.position - point.position;
        distance.y = 0;
        Vector3 speed = distance / time_2;
        velocity = speed + time_1 * Gravity * Vector3.up;
    }

    private void FirePreloadJudgment()
    {
        if (prefabsTypeNum == 1)
        {
            FirePreload((GameObject[])PrefabList[0]);
        }
        else if (prefabsTypeNum == 2)
        {
            FirePreload((GameObject[])PrefabList[0], (GameObject[])PrefabList[1]);
        }
        else if (prefabsTypeNum == 3)
        {
            FirePreload((GameObject[])PrefabList[0], (GameObject[])PrefabList[1], (GameObject[])PrefabList[2]);
        }
    }

    private void FirePreload(GameObject[] tyope1)
    {
        point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

        int p1 = Random.Range(0, tyope1.Length);
        bullet = Instantiate(tyope1[p1], point.position, Quaternion.identity);

        loopTimes = type1MaxNum - 1;
        if (loopTimes > 0)
        {
            loopGroup = tyope1;
        }
    }

    private void FirePreload(GameObject[] tyope1, GameObject[] tyope2)
    {
        point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

        int p0 = Random.Range(1, randomrange);

        if (p0 < randomrangeLeft)
        {
            int p1 = Random.Range(0, tyope1.Length);
            bullet = Instantiate(tyope1[p1], point.position, Quaternion.identity);

            loopTimes = type1MaxNum - 1;
            if (loopTimes > 0)
            {
                loopGroup = tyope1;
            }
        }
        else
        {
            int p2 = Random.Range(0, tyope2.Length);
            bullet = Instantiate(tyope2[p2], point.position, Quaternion.identity);

            loopTimes = type2MaxNum - 1;
            if (loopTimes > 0)
            {
                loopGroup = tyope2;
            }
        }
    }

    private void FirePreload(GameObject[] tyope1, GameObject[] tyope2, GameObject[] tyope3)
    {
        point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

        int p0 = Random.Range(1, randomrange);


        if (p0 < randomrangeLeft)
        {
            int p1 = Random.Range(0, tyope1.Length);
            bullet = Instantiate(tyope1[p1], point.position, Quaternion.identity);

            loopTimes = type1MaxNum - 1;
            if(loopTimes > 0)
            {
                loopGroup = tyope1;
            }
        }
        else if (p0 >= randomrangeLeft && p0 < randomrangeRight)
        {
            int p2 = Random.Range(0, tyope2.Length);
            bullet = Instantiate(tyope2[p2], point.position, Quaternion.identity);

            loopTimes = type2MaxNum - 1;
            if (loopTimes > 0)
            {
                loopGroup = tyope2;
            }
        }
        else
        {
            int p3 = Random.Range(0, tyope3.Length);
            bullet = Instantiate(tyope3[p3], point.position, Quaternion.identity);

            loopTimes = type3MaxNum - 1;
            if (loopTimes > 0)
            {
                loopGroup = tyope3;
            }
        }
    }

    void Fire()
    {
        //point.rotation = Quaternion.FromToRotation(Vector3.up, velocity);
        bullet.SendMessage("FireParationA");
        bullet.SendMessage("Shoot", velocity);
        bullet.SendMessage("FiredByOtherRotation", (point.position - velocity).sqrMagnitude);
    }

    void Loop()
    {
        int p1 = Random.Range(0, loopGroup.Length);
        bullet = Instantiate(loopGroup[p1], point.position, Quaternion.identity);

        //realIntervalTime = 0.2f;
        //StartCoroutine(FireInterval());

        loopTimes--;
    }

    void SlowMotion()
    {
        if (isSlowMotion)
        {
            if (Time.timeScale != timeZoomRate)
            {
                Time.timeScale = timeZoomRate;
                Time.fixedDeltaTime = defultFixedDeltaTime * Time.timeScale;
            }
        }
        else
        {
            timeZoomRate = 1F;
            Time.timeScale = 1F;
        }
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
        Time.fixedDeltaTime = defultFixedDeltaTime;
        timeZoomRate = 1F;
        Time.timeScale = 1F;
    }

    //private void OnDestroy()
    //{
    //    if (Time.timeScale != 1F)
    //    {
    //        timeZoomRate = 1F;
    //        Time.timeScale = 1F;
    //    }
    //}
}
