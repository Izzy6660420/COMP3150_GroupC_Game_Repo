using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public int doorTag;
    public string sceneTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && Input.GetButtonDown(InputAxes.Interact))
        {
            SceneManage sc = new SceneManage();
            sc.SceneLoad(doorTag);
        }
    }

    public string getSceneTag
    {
        get
        {
            return sceneTag;
        }
    }
}
