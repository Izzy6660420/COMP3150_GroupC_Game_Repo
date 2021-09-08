using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicUI : MonoBehaviour
{
    public static PanicUI instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of PanicUI detected!");
        }
        instance = this;
    }

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Camera uiCam;
    
    public void SetPanicCeiling(float max, float min)
    {
        slider.maxValue = max/100.0f;
        slider.minValue = min/100.0f;
        
        fill.color = gradient.Evaluate(1f);
    }

    public void SetPanic(float power)
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
