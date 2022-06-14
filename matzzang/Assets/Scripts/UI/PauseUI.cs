using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public EnemyAimV3Line enemyAimV3Line;
    public AirPlaneFlyForThrower airPlaneFlyForThrower;
    public GameObject leftHandRay;
    public GameObject rigihtHandRay;

    public GameObject ui1;
    public GameObject ui2;

    private bool isPressing = false;
    private bool isTurnOff = false;

    private void Update()
    {
        if (isPressing)
        {
            Destroy(GameObject.FindGameObjectWithTag("Bottle"));
        }

        if(ui1.activeInHierarchy || ui2.activeInHierarchy)
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        if (enemyAimV3Line)
        {
            enemyAimV3Line.isPaused = true;
            leftHandRay.SetActive(true);
            rigihtHandRay.SetActive(true);
            isPressing = true;
            airPlaneFlyForThrower.enabled = false;
        }
    }

    private void OnDisable()
    {
        enemyAimV3Line.isPaused = false;

        if (isTurnOff)
        {
            leftHandRay.SetActive(false);
            rigihtHandRay.SetActive(false);
            isPressing = false;
            airPlaneFlyForThrower.enabled = true;
            isTurnOff = false;
        }
    }

    public void TurnOff()
    {
        isTurnOff = true;
        gameObject.SetActive(false);
    }
}
