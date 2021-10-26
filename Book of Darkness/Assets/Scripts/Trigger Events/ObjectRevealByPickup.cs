using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRevealByPickup : CustomizedMajorEvent
{

    [SerializeField]
    public GameObject revealItem;
    
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
        gameObject.SetActive(false);
    }
}
