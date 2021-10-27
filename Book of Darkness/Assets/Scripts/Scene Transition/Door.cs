using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
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

    public override void Interact(Collider2D col)
    {
        if (!player.canEnter)
            return;

        player.canEnter = false;
        SpawnManager.instance.Warp(destination);
        AudioManager.instance.PlaySound("Door", transform.position, 0.3f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!Player.instance.CompareScene(sceneNames[1]))
        {
            player.canEnter = true;
            SetInteracted(false);
        }
    }
}