using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactable
{
    private Animator animatorD;
    private Player player;

    void Start()
    {
        player = Player.instance;
        animatorD = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animatorD.SetBool("Player_Hidden", player.IsHiding());
    }

    public override void Interact(Collider2D col)
    {
        tooltip.SetActive(false);
    }
}
