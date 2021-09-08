using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of SceneManager detected!");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private string fromScene;
    private Transform player;

    void Start()
    {
        player = CharacterController2D.instance.transform;
    }

    public void AppendOriginScene(string sceneName)
    {
        fromScene = sceneName;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fromScene != null)
        {
            //  Look for door and set spawn
            Vector3 spawnPoint = GameObject.Find(fromScene).transform.position;

            // Move player to spawn
            player.position = spawnPoint;

            Debug.Log("Loaded from: " + fromScene + " at " + spawnPoint);
        }
        fromScene = null;
    }
}
