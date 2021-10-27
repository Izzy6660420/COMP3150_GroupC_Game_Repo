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
        monster.SetActive(true);
        yield return new WaitForSeconds(2);

        monster.GetComponent<EnemyAI>().enabled = true;
        gameObject.SetActive(false);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
