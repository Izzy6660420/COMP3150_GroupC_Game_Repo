using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirstReveal : CustomizedMajorEvent
{
    [SerializeField]
    public GameObject eventSystem, monster, teddyBear, rippedUpTeddy;

    public override IEnumerator customEvent()
    {    
        DimensionController.instance.CameraSwitch();
        monster.SetActive(true);
        teddyBear.SetActive(false);
        rippedUpTeddy.SetActive(true);

        yield return new WaitForSeconds(1);

        monster.GetComponent<EnemyAI>().enabled = true;
        DimensionController.instance.CameraSwitch();

        yield return new WaitForSeconds(0.5f);

        DimensionController.instance.CameraSwitch();
        
        eventSystem.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
