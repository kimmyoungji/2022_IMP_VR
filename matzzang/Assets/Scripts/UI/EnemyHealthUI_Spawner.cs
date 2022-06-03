using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI_Spawner : MonoBehaviour
{
    [SerializeField]
    private UIManager UIManager;

    [SerializeField]
    private GameObject EnemyHealthUIprefab;
    private GameObject EnemyHealthUI;

    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        EnemyHealthUI = Instantiate(EnemyHealthUIprefab,transform);
        UIManager.SendMessage("setEnemyHealthUI",EnemyHealthUI);
    }

}
