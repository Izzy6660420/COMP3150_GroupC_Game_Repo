using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickerControl : MonoBehaviour
{
    new private Light2D light;

    [Range(0.0f, 10.0f)]
    public float MinimumTimeDelay, MaximumTimeDelay, MinimumLightIntensity, MaximumLightIntensity;

    private float timeDelay;

    public bool flicker;

    void Start()
    {
        light = gameObject.GetComponent<Light2D>();

        timeDelay = Random.Range(MinimumTimeDelay, MaximumTimeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if(flicker) FlickeringLight();
    }

    void FlickeringLight()
    {
        if(timeDelay > 0) timeDelay -= Time.deltaTime;

        else
        {
            light.enabled = !light.enabled;
            light.intensity = Random.Range(MinimumLightIntensity, MaximumLightIntensity);

            timeDelay = Random.Range(MinimumTimeDelay, MaximumTimeDelay);
        }
    }
}
