using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue[] dialogues;
    public Item requiredItem;
    public ItemGroup itemGroup;

    bool talkedTo = false;
    bool recievedItem = false;
    bool moved = false;

    public Transform target;

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
                    if (!moved) StartCoroutine(Move());
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
            i = 0;
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

    IEnumerator Move()
    {
        float step = 10 * Time.deltaTime;

        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            Debug.Log("Moved");
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            yield return new WaitForSeconds(10f * Time.deltaTime);
        }

        moved = true;
        yield return null;
    }
}
