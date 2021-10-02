using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

	public override void Interact(Collider2D col)
    {
        PickUp();
    }

    void PickUp()
    {
        bool pickedUp = Inventory.instance.Add(item);
        if (pickedUp)
        {
            Destroy(gameObject);
        }
    }
}
