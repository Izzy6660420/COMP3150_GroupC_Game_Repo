using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    public float charge = 20f;

    public override void Interact(Collider2D col)
    {
        var power = Player.instance.torch.AddPower(charge);
        TorchUI.instance.SetBattery(power);
        Destroy(gameObject);
    }
}
