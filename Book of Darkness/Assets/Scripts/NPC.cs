using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue dialogue;
    bool talkedTo = false;
    public Item requiredItem;

    public override void Interact(Collider2D col)
    {
        if (talkedTo)
        {
            if (requiredItem != null)
            {
                if (Inventory.instance.HasItem(requiredItem.name))
                {
                    // Player has required item.
                }
            }
        }
        else
        {
            Talk();
        }
    }

    void Talk()
    {
        DialogueManager.instance.StartDialogue(dialogue);
        talkedTo = true;
    }
}
