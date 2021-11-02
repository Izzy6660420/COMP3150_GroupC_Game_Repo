using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRevealByPickup : CustomizedMajorEvent
{

    [SerializeField]
    public GameObject revealItem, nextEvent, teddyBear, rippedUpTeddy;
    
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
        yield return new WaitForSeconds(0);
    }

    public override void triggerEvent()
    {
        revealItem.SetActive(true);
        teddyBear.SetActive(false);
        rippedUpTeddy.SetActive(true);
        nextEvent.SetActive(true);
        gameObject.SetActive(false);
    }
}
