using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Singleton
[Serializable]
public struct SpawnPoint 
{
    public int doorTag;
    public string connectedTo;
}

public class SceneManage : MonoBehaviour
{
    static private SceneManage instance;
    [SerializeField]
    
    static public SceneManage Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("There are no SceneManager in the scene");
            }

            return instance;
        }
    }

    void Awake()
    {
        
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("There are more than one SceneManager in the scene");
        }
        else
        {
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public SpawnPoint[] spawnPoints;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoad(int tag)
    {   
        string connect = "";
        foreach(SpawnPoint spawn in spawnPoints)
        {
            if(spawn.doorTag == tag) {
                SceneManager.LoadScene(spawn.connectedTo);
                connect = spawn.connectedTo;
            }
        }

        TransitionPoint[] tps = FindObjectsOfType<TransitionPoint>();
        foreach(TransitionPoint tp in tps)
        {
            if(tp.getSceneTag == connect)
            {
                player.transform.position = tp.gameObject.transform.position;
            }
        }
        
    }
}
