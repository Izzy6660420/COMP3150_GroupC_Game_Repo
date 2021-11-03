using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromEnemy : CustomizedMajorEvent
{
    [SerializeField]
    public GameObject monster;
    private Collider2D collide;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) triggerEvent();
    }

    public override IEnumerator customEvent()
    {
        collide = gameObject.GetComponent<Collider2D>();
        collide.enabled = false;
        if (DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        monster.SetActive(true);
        AudioManager.instance.PlaySound("Enemy Reveal", Vector3.zero, 0.8f);
        yield return new WaitForSeconds(0.5f);

        if(DimensionController.instance.dimensionStr != DimensionController.nightmareStr) DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        if(DimensionController.instance.dimensionStr != DimensionController.darknessStr) DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;
        Destroy(gameObject);
    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
