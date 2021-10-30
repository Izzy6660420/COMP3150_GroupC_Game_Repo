using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    public Item item;
    SpriteRenderer sRenderer;
    public event Action<Food> itemChosenEvent;

    void Start()
    {
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Interact(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound("Item Pickup", transform.position, .6f);
            if (GetComponent<Renderer>().enabled)
            {
                Inventory.instance.Add(item);
                sRenderer.enabled = false;
                itemChosenEvent(this);
            }
            else
            {
                Drop();
            }
        }
    }

    public void Drop()
    {
        Inventory.instance.Remove(item);
        sRenderer.enabled = true;
    }
}
