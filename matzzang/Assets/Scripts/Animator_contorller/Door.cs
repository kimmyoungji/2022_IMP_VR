using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform target;
    [SerializeField] float aimDistance = 5;
    Animator animator;
    float distance;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = (transform.position - target.position).sqrMagnitude;
        if(distance < aimDistance)
        {
            animator.SetTrigger("playerClose");
        }
    }
}
