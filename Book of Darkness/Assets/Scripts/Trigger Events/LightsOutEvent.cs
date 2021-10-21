using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsOutEvent : TriggerEvents
{
    
    public enum LightEvents
    {
        LightFlicker,
        LightTurnOff,
        LightFlickerAndTurnOff,
        LightTurnOn
    };

    [SerializeField]
    public LightEvents lightEvents;
    public Light2D light;
    public float flickerTimer;

    private float timer;
    private bool triggered = false;

    public void Update()
    {
        if(lightEvents == LightEvents.LightFlickerAndTurnOff && triggered)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                //Debug.Log("hi");
            }
            else
            {
                light.GetComponent<FlickerControl>().flicker = false;
                light.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) 
        {
            if(!triggered) collidersEnable();

            switch(lightEvents)
            {
                case LightEvents.LightTurnOff:
                light.GetComponent<FlickerControl>().flicker = false;
                light.enabled = false;
                break;

                case LightEvents.LightFlicker:
                light.GetComponent<FlickerControl>().flicker = true;
                break;

                case LightEvents.LightFlickerAndTurnOff:
                light.GetComponent<FlickerControl>().flicker = true;
                timer = flickerTimer;
                triggered = true;
                break;
            }

            if(timer <= 0) 
            {
                gameObject.SetActive(false);
            }


        }
    }

    public override void OnTriggerExit2D(Collider2D col)
    {

    }

    public override void OnTriggerStay2D(Collider2D col)
    {
    
    }

    public override void collidersEnable()
    {
        foreach(GameObject go in secondaryColliders)
        {
            go.SetActive(true);
        }
    }
}
