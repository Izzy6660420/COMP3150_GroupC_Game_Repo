using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    public float charge = 0.2f;

    public override void Interact(Collider2D col)
    {
        Player.instance.torch.AddPower(charge);
        Destroy(gameObject);
    }
}
