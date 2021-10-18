using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue[] dialogues;
    bool talkedTo = false;
    public Item requiredItem;
    public ItemGroup itemGroup;
    bool recievedItem = false;

    public override void Interact(Collider2D col)
    {
        int i = 0;
        if (talkedTo)
        {
            if (requiredItem != null)
            {
                if (Inventory.instance.HasItem(requiredItem.name))
                {
                    i = 2;
                    recievedItem = true;
                }
                else
                {
                    var hasItemFromGroup = false;
                    foreach (Food item in itemGroup.items)
                    {
                        if (Inventory.instance.HasItem(item.item.name))
                            hasItemFromGroup = true;
                    }

                    i = hasItemFromGroup ? 1 : 3;
                }
            }
        }
        else
        {
            i = 3;
        }

        if (recievedItem)
            i = 4;
        Talk(dialogues[i]);
    }

    void Talk(Dialogue dialogue)
    {
        DialogueManager.instance.StartDialogue(dialogue);
        talkedTo = true;
    }
}
