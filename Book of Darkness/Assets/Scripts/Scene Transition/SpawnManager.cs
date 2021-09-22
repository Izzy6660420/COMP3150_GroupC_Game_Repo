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
    private string scene = "Bedroom";

    void Start()
    {
        player = CharacterController2D.instance.transform;
    }

    public void Warp(Transform destination)
    {
        Debug.Log("Current scene: " + scene);
        player.position = destination.position;
        scene = destination.transform.name;

        CharacterController2D.instance.canEnter = false;
    }

    public bool MatchScene(string name)
    {
        return name != scene;
    }
}
