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
    float score = 0;
    [SerializeField] int aimScore = 20;
    float scoreLast;
    //float objectLast;

    WaitForSeconds waitForSeconds;

    bool isStopMove = false;

    float EnemyHealthLoss = 50;

    void Awake()
    {
        originPos = this.gameObject.transform.position;
        //Grabs = GameObject.FindGameObjectsWithTag("Trash");
    }

    private void Start()
    {
        GetRandomPosition();
        scoreLast = aimScore;
        //grabNum = Grabs.Length;
        //objectLast = Grabs.Length;
        //waitForSeconds = new WaitForSeconds(3f);
    }

    void Update()
    {
        //if (score >= grabNum)
        //{
        //    transform.position = originPos;
        //    return;
        //}
        //else
        //{
        //    distance = (transform.position - randomPos).sqrMagnitude;
        //    if (distance > 0.01f)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, randomPos, flySpeed * Time.deltaTime);
        //    }
        //    else
        //    {
        //        GetRandomPosition();
        //    }
        //}

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bottle"))
        {
            //score++;
            //scoreLast--;
            GameManager.SendMessage("UpdateEnemyHealth",EnemyHealthLoss);
            //objectLast--;
            //Debug.Log("You Hit!  -  Score: " + score);
            //Debug.Log("Enemy HP Left: " + scoreLast);
            //Debug.Log("Trash objects left: " + objectLast);
            AudioManager.Instance.PlayRandomSFX(hitAudio);

            //if (score >= grabNum)
            // if (score >= aimScore)
            // {
            //     Debug.Log("Level Cleared");
            //     StartCoroutine(nameof(WaitTime));
            // }
        }
    }

    // IEnumerator WaitTime()
    // {
    //     yield return waitForSeconds;

    //     //EditorApplication.isPaused = true;
    //     EditorApplication.isPlaying = false;

    //     //Time.timeScale = 0;
    // }

    public void IsStopMove(bool isStop)
    {
        isStopMove = isStop;
    }
}
