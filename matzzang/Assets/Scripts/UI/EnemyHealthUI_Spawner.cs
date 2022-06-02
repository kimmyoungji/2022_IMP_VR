using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI_Spawner : MonoBehaviour
{
    public GameObject UIManager;
    public GameObject EnemyHealthUIprefab;
    private GameObject EnemyHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealthUI = Instantiate(EnemyHealthUIprefab,transform);
        UIManager.SendMessage("setEnemyHealthUI",EnemyHealthUI);
    }

}
