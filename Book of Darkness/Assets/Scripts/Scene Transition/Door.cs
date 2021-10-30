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
        SpawnManager.instance.Warp(destination);
        AudioManager.instance.PlaySound("Door", transform.position, 0.3f);
    }
}