using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private CharacterController2D player;

    void Start()
    {
        player = CharacterController2D.instance;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!player.canEnter)
            return;

        var currentScene = SceneManager.GetActiveScene().name;
        var nextScene = this.name;

        if (other.tag == "Player")
        {
            //  Don't destroy player
            DontDestroyOnLoad(other.gameObject);

            // Stops infinite loading loop
            player.canEnter = false;

            //  Load the scene
            StartCoroutine(loadScene(other, currentScene, nextScene));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        player.canEnter = true;
    }

    IEnumerator loadScene(Collider2D other, string currentScene, string nextScene)
    {
        // Save current scene name
        SpawnManager.instance.AppendOriginScene(currentScene);

        //  Load next scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading scene...");
            yield return null;
        }
    }
}