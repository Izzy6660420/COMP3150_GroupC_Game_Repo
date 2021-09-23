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
    }

    private Transform player;

    void Start()
    {
        player = Player.instance.transform;
    }

    public void Warp(Transform destination)
    {
        string previousScene;
        string currentScene;
        string[] sceneNames = destination.name.Split(char.Parse(":"));

        previousScene = sceneNames[1];
        currentScene = sceneNames[0];
        Debug.Log("Warped to: " + currentScene);

        player.position = destination.position;
        Player.instance.ChangeScene(currentScene);

        Player.instance.canEnter = false;
    }
}
