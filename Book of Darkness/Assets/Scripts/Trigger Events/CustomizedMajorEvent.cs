using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizedMajorEvent : MonoBehaviour
{
    [SerializeField]
    public GameObject eventSystem, monster;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator customEvent()
    {    
        DimensionController.instance.CameraSwitch();
        monster.SetActive(true);
        yield return new WaitForSeconds(2);
        DimensionController.instance.CameraSwitch();
        yield return new WaitForSeconds(1);

        DimensionController.instance.CameraSwitch();
        monster.GetComponent<EnemyAI>().enabled = true;

        gameObject.SetActive(false);

    }

    public void triggerEvent()
    {
        StartCoroutine(customEvent());
    }
}
