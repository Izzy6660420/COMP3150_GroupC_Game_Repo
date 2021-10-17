using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue[] dialogues;
    bool talkedTo = false;
    public Item requiredItem;
    public ItemGroup itemGroup;

    public override void Interact(Collider2D col)
    {
        if (talkedTo)
        {
            if (requiredItem != null)
            {
                if (Inventory.instance.HasItem(requiredItem.name))
                {
                    Talk(dialogues[2]);
                }
                else
                {
                    var hasItemFromGroup = false;
                    foreach (Food item in itemGroup.items)
                    {
                        if (Inventory.instance.HasItem(item.item.name))
                            hasItemFromGroup = true;
                    }

                    if (hasItemFromGroup)
                    {
                        Talk(dialogues[1]);
                    }
                    else
                    {
                        Talk(dialogues[3]);
                    }
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
