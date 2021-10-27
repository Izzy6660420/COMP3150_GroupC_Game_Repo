using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanic : MonoBehaviour
{
    public static PlayerPanic instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of PlayerPanic detected!");
        }
        instance = this;
    }

    private float value = 0.0f;
    private float maxValue = 10.0f;
    public float drainSpeed = 1.0f;
    DimensionController dim;

    void Start()
    {
        dim = DimensionController.instance;
    }

    void Update()
    {
        value = Mathf.Clamp(value, 0, maxValue);
        if (dim.darkness.enabled) { value += Time.deltaTime * drainSpeed; }
    }

    public float GetPercent()
    {
        return 1 - value / maxValue;
    }
}
