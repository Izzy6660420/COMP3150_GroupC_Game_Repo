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

    public override void Interact()
    {
        Inventory.instance.AddBattery();
        Destroy(gameObject);
    }
}
