using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.5f;
    public int defaultValue;
    public int upgradeStep;
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
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed;
        }
        else if (slider.value > targetProgress)
        {
            slider.value -= fillSpeed;
        }
    }

    public void SetValue(int curVal, int maxVal)
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        if (maxVal > defaultValue)
        {
            int add = (maxVal - defaultValue) / upgradeStep;
            rect.offsetMax = new Vector2(-1529+50*add, rect.offsetMax.y);
        }

        var percent = curVal * 100 / maxVal;
        targetProgress = percent;
    }

    public void HideFill()
    {
        slider = GetComponent<Slider>();
        slider.fillRect.gameObject.SetActive(false);
    }
}
