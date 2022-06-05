using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet3Bottle : MonoBehaviour
{
    [SerializeField] float changeWaitTime = 0.3f;

    private Rigidbody rb;

    [Header("SlowMotion")]
    public bool isSlowMotion = true;
    public float timeZoomRate = 0.5F;

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(changeWaitTime);

    //    GetComponent<SphereCollider>().enabled = false;
    //    GetComponent<MeshCollider>().enabled = true;
    //    GetComponent<MeshCollider>().isTrigger = false;
    //}

    public void Shoot(Vector3 velocity)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(velocity, ForceMode.Impulse);
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    gameObject.SetActive(false);
    //}

    private void FireParationA()
    {
        StartCoroutine(nameof(FireParationB));
    }
    IEnumerator FireParationB()
    {
        yield return new WaitForSeconds(changeWaitTime);

        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<MeshCollider>().isTrigger = false;
    }


    private void Update()
    {
        if (isSlowMotion)
        {
            if(Time.timeScale != timeZoomRate)
            Time.timeScale = timeZoomRate;
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
        if (Time.timeScale != 1F)
        {
            Time.timeScale = 1F;
        }
    }
}
