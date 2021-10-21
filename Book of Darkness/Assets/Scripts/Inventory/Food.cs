using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    public Item item;
    SpriteRenderer renderer;
    public event Action<Food> itemChosenEvent;

    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Interact(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (renderer.enabled)
            {
                Inventory.instance.Add(item);
                renderer.enabled = false;
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
        renderer.enabled = true;
    }
}
