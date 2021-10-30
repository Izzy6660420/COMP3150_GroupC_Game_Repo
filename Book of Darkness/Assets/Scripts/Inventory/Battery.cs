using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    public float charge = 20f;
    public override void Interact(Collider2D col)
    {
        var power = PlayerTorch.instance.AddPower(charge);
        AudioManager.instance.PlaySound("Item Pickup", transform.position, .6f);
        Destroy(gameObject);
    }
}
