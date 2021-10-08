using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactable
{
    public Item item;
    new SpriteRenderer renderer;

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
            }
            else
            {
                Inventory.instance.Remove(item);
                renderer.enabled = true;
            }
        }
    }
}
