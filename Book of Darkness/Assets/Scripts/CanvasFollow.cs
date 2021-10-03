using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

    void Start()
    {
        player = Player.instance.transform;
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
