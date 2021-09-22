using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private CharacterController2D player;
    public Transform destinationTransform;

    void Start()
    {
        player = CharacterController2D.instance;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (!player.canEnter)
            return;

            if (other.tag == "Player" && Input.GetButton(InputAxes.Interact))
        {
            // Stops infinite loading loop
            player.canEnter = false;
            SpawnManager.instance.Warp(destinationTransform);
            
            Debug.Log("Warp to: " + destinationTransform.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (SpawnManager.instance.MatchScene(destinationTransform.name))
        {
            player.canEnter = true;
        }
    }
}