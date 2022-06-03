using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public RectTransform Fill;
    private float FillWidth;
    private float NewWidth;
    private bool isUpdated;

    private float decrSpeed = 4;
    
    void Start()
    {
        FillWidth = 160;
        NewWidth = 160;
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
