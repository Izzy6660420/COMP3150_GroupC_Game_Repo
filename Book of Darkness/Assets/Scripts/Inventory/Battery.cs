using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
    }

    public override void Interact(Collider2D col)
    {
        Inventory.instance.AddBattery();
        Destroy(gameObject);
    }
}
