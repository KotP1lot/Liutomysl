using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.5f;
    private float targetProgress = 100;
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value != targetProgress)
        {
            if (slider.value < targetProgress)
            {
                slider.value += fillSpeed;
            }
            else if (slider.value > targetProgress)
            {
                slider.value -= fillSpeed;
            }
        }
        
        
        
    }

    public void SetValue(int curVal, int maxVal)
    {
        if (maxVal > 100)
        {
            int add = (maxVal - 100) / 50;
            rect.offsetMax = new Vector2(-1529+30*add, rect.offsetMax.y);
        }

        var percent = curVal * 100 / maxVal;
        targetProgress = percent;
    }
}
