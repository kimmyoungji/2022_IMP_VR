using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    public RectTransform Fill;
    private float FillWidth;
    private float NewWidth;
    private bool isUpdated;

    private float decrSpeed = 4;
    
    void Start()
    {
        FillWidth = 160f;
        NewWidth = 160f;
        Fill.sizeDelta = new Vector2(FillWidth, 10f);
    }

    private void Update() {
        FillWidth -= ( FillWidth - NewWidth ) * Time.deltaTime*decrSpeed;
        Fill.sizeDelta = new Vector2(FillWidth, 10f);
    }

    public void UpdateFill(float width){
        NewWidth = width;
    }

}
