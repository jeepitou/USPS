using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    public void SetValue(float newValue)
    {
        _slider.value = newValue;
    }

    public void AddToValue(float valueToAdd)
    {
        _slider.value += valueToAdd;

        if (_slider.value > _slider.maxValue)
        {
            _slider.value = _slider.maxValue;
        }
        else if (_slider.value < 0)
        {
            _slider.value = 0;
        }

    }
}
