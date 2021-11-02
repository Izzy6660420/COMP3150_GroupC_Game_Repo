using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromEnemy : CustomizedMajorEvent
{
    [SerializeField]
    public GameObject monster;

    void OnTriggerEnter2D()
    {
        triggerEvent();
    }

    public override IEnumerator customEvent()
    {    
        if(DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        monster.SetActive(true);
        AudioManager.instance.PlaySound("Enemy Reveal", Vector3.zero, 0.8f);
        yield return new WaitForSeconds(0.5f);

        if(DimensionController.instance.dimensionStr != DimensionController.nightmareStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        if(DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;
        gameObject.SetActive(false);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
