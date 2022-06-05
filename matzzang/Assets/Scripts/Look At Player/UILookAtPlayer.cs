using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtPlayer : MonoBehaviour
{
    public Transform targetPlayer;

    private Vector3 realLookPosition;

    void Update()
    {
        realLookPosition = new Vector3(targetPlayer.position.x, targetPlayer.position.y, targetPlayer.position.z);
        transform.LookAt(realLookPosition);

        transform.position = new Vector3(transform.position.x, targetPlayer.position.y, transform.position.z);
    }
}
