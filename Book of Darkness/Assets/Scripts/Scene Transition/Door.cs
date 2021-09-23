using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Player player;
    private Transform destination;
    string[] sceneNames;

    void Start()
    {
        player = Player.instance;

        sceneNames = transform.name.Split(char.Parse(":"));
        destination = GameObject.Find(sceneNames[1] + ":" + sceneNames[0]).transform;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (!player.canEnter)
            return;

            if (other.tag == "Player" && Input.GetButton(InputAxes.Interact))
        {
            // Stops infinite loading loop
            player.canEnter = false;
            SpawnManager.instance.Warp(destination);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (!Player.instance.CompareScene(sceneNames[1]))
        {
            player.canEnter = true;
        }
    }
}