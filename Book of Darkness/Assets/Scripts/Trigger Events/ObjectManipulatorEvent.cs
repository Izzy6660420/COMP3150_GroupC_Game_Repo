using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectManipulation {
    public enum omBool {
        Enable,
        Disable
    };

    public GameObject omObject;
    public omBool omBoolean;
    
}

public class ObjectManipulatorEvent : TriggerEvents
{
    

    [SerializeField]
    public List<ObjectManipulation> objectMan;

    private bool triggered;

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) 
        {
            if(!triggered) collidersEnable();

            triggered = true;
            foreach(ObjectManipulation om in objectMan)
            {
                switch(om.omBoolean)
                {
                    case ObjectManipulation.omBool.Enable:
                    om.omObject.SetActive(true);
                    break;

                    case ObjectManipulation.omBool.Disable:
                    om.omObject.SetActive(false);
                    break;
                }
            }
        }

        gameObject.SetActive(false);

 
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
