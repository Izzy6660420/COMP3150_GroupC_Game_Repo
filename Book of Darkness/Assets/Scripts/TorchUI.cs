using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TorchUI : MonoBehaviour
{   
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Camera uiCam;
    
    public void SetBatteryCeiling(float maxPow, float minPow)
    {
        slider.maxValue = maxPow/100.0f;
        slider.minValue = minPow/100.0f;
        
        fill.color = gradient.Evaluate(1f);
    }

    public void SetBattery(float power)
    {
        slider.value = power/100.0f;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void setLocation(Vector3 location)
    {
        transform.position = location;
    }

    public void SetCamera(Camera cam)
    {
        uiCam = cam;
    }
}
