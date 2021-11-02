using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirstReveal : CustomizedMajorEvent
{
    [SerializeField]
    public GameObject eventSystem, monster;

    public override IEnumerator customEvent()
    {    
        DimensionController.instance.CameraSwitch();
        monster.SetActive(true);


        AudioManager.instance.PlaySound("Enemy Reveal", Vector3.zero, 0.8f);

        yield return new WaitForSeconds(1f);

        DimensionController.instance.CameraSwitch();
        
        Destroy(monster);
        eventSystem.SetActive(true);
        gameObject.SetActive(false);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
