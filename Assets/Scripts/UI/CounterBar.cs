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
        slider.maxValue=val;
        slider.value=val;
            
        rect.offsetMin = new Vector2(1741-12*(val- defaultValue), rect.offsetMin.y);
    }

    public void Upgrade()
    {
        SetValue((int)slider.value + upgradeStep);
    }
}
