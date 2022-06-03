using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirPlaneFlyForThrower_v1 : MonoBehaviour
{
    //GameManager
    [SerializeField]
    GameManager GameManager;
    
    [SerializeField] float flySpeed = 2.0f;
    [SerializeField] AudioData hitAudio;

    // Enemy Movement var
    Vector3 originPos;
    float distance;
    Vector3 randomPos;
    bool isStopMove = false;

    // Health loss
    [Header("Health loss")]
    public float EnemyHealthLoss = 20;

    // Animator
    [Header("Animator")]
    [SerializeField]
    public Animator animator;
    private Vector3 initGap;
    private Vector3 gap;

    // start
    private void Start(){ 
        GameManager = GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        originPos = this.gameObject.transform.position;
        GetRandomPosition();
    }

    void Update()
    {
        if (!isStopMove)
        {
            gap = transform.position - randomPos;
            float temp_x = ( gap.x/initGap.x );
            animator.SetFloat("WalkLeft",temp_x);
            distance = (gap).sqrMagnitude;
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
        initGap = transform.position - randomPos;
        animator.SetFloat("isWalking",initGap.x);
        //randomPos = new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 8f), originPos.z);
        //randomPos = new Vector3(Random.Range(-10f, 10f), 5, originPos.z);
    }

    public void IsStopMove(bool isStop)
    {
        isStopMove = isStop;
    }
}
