using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintMeter : MonoBehaviour
{

    public Slider meter;
   

    public void SetMaxValue(float meterValue)
    {
        meter.maxValue = meterValue;
        meter.value = meterValue;
    }

    public void SetMeter(float meterValue)
    {
        meter.value = meterValue;
    }
    
}
