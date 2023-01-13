using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterBar : MonoBehaviour
{
    private Slider slider;
    public int defaultValue=3;
    public int upgradeStep=1;
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        slider.maxValue = defaultValue;
        slider.value= defaultValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int val)
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        if (slider != null)
        {
            slider.maxValue = val;
            slider.value = val;
        }
            
        rect.offsetMin = new Vector2(1741-12*(val- defaultValue), rect.offsetMin.y);
    }

    public void SetValueFromPlayerData(int value, int upgradeStep)
    {
        SetValue((value / upgradeStep)+defaultValue);
    }

    public void SetValueFromPlayerData(float value, float upgradeStep)
    {
        SetValue(Mathf.CeilToInt(((value-1) / upgradeStep) + defaultValue));
    }

    public void Upgrade()
    {
        SetValue((int)slider.value + upgradeStep);
    }
}
