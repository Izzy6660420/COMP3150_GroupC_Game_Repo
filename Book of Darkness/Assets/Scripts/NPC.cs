using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue[] dialogues;
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
            Talk(dialogues[0]);
        }
    }

    void Talk(Dialogue dialogue)
    {
        DialogueManager.instance.StartDialogue(dialogue);
        talkedTo = true;
    }
}
