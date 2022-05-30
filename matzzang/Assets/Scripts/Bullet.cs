using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int power = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Trash"))
        {
            collision.transform.SendMessage("GetDamage", power);
        }
    }
}
