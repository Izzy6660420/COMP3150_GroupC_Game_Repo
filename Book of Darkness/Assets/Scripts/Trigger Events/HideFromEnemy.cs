using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromEnemy : CustomizedMajorEvent
{
    [SerializeField]
    public GameObject monster;
    bool triggered;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (triggered) return;
        if (col.gameObject.TryGetComponent(out Player player)) triggerEvent();
    }

    public override IEnumerator customEvent()
    {
        triggered = true;

        if (DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(0.5f);

        monster.SetActive(true);
        AudioManager.instance.PlaySound("Enemy Reveal", Vector3.zero, 0.8f);
        yield return new WaitForSeconds(0.5f);

        if(DimensionController.instance.dimensionStr != DimensionController.nightmareStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        if(DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
