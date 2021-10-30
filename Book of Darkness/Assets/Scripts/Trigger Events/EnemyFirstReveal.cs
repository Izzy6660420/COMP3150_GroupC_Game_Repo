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

        AudioManager.instance.PlaySound("Enemy Reveal", Vector3.zero, 0.8f);

        yield return new WaitForSeconds(0.8f);

        DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;

        yield return new WaitForSeconds(0.3f);

        DimensionController.instance.CameraSwitch();
        
        eventSystem.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
