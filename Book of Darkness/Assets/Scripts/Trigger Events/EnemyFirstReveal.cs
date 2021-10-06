using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirstReveal : CustomizedMajorEvent
{

    [SerializeField]
    public GameObject eventSystem, monster;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator customEvent()
    {    
        DimensionController.instance.CameraSwitch();
        monster.SetActive(true);
        yield return new WaitForSeconds(2);
        DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;

        eventSystem.SetActive(true);
        gameObject.SetActive(false);

    }

    public override void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
