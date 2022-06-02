using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirPlaneFlyForThrower : MonoBehaviour
{
    //GameManager
    public GameObject GameManager;
    
    [SerializeField] float flySpeed = 5.0f;
    [SerializeField] AudioData hitAudio;

    Vector3 originPos;
    float distance;
    Vector3 randomPos;

    //GameObject[] Grabs;
    //float grabNum;
    // float score = 0;
    // [SerializeField] int aimScore = 20;
    // float scoreLast;
    //float objectLast;

    //WaitForSeconds waitForSeconds;
    bool isStopMove = false;

    public float EnemyHealthLoss = 20;

    void Awake()
    {
        originPos = this.gameObject.transform.position;
        //Grabs = GameObject.FindGameObjectsWithTag("Trash");
    }

    private void Start()
    {
        GetRandomPosition();
        //scoreLast = aimScore;
        //grabNum = Grabs.Length;
        //objectLast = Grabs.Length;
        //waitForSeconds = new WaitForSeconds(3f);
    }

    void Update()
    {
        if (!isStopMove)
        {
            distance = (transform.position - randomPos).sqrMagnitude;
            if (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, randomPos, flySpeed * Time.deltaTime);
            }
            else
            {
                GetRandomPosition();
            }
        }
    }

    void GetRandomPosition()
    {
        randomPos = new Vector3(Random.Range(-3f, 3f), originPos.y, originPos.z);
        //randomPos = new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 8f), originPos.z);
        //randomPos = new Vector3(Random.Range(-10f, 10f), 5, originPos.z);
    }

    public void IsStopMove(bool isStop)
    {
        isStopMove = isStop;
    }
}
