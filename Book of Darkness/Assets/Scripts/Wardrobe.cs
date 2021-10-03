using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    private Animator animator;
    private Player player;

    void Start()
    {
        player = Player.instance;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetBool("Player_Hidden", player.IsHiding());
    }
}
