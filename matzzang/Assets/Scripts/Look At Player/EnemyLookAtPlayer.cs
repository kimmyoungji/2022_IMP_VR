using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform targetPlayer;

    private Vector3 realLookPosition;

    private void Start()
    {

    }

    void Update()
    {
        realLookPosition = new Vector3(targetPlayer.position.x, 0, targetPlayer.position.z);
        transform.LookAt(realLookPosition);
    }
}
