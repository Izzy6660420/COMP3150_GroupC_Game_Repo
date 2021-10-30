using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearNPC : NPC
{
    public Item requiredItem;
    public ItemGroup itemGroup;

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

                    if (!moved)
                    {
                        recievedItem = true;
                        Inventory.instance.Remove(requiredItem);
                        StartCoroutine(Move());
                    }
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

        if (recievedItem && moved)
            i = 4;
        Talk(dialogues[i]);
    }

    IEnumerator Move()
    {
        float step = 10 * Time.deltaTime;
        //AudioManager.instance.PlaySound("Bear Move", transform.position, 0.8f);

        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            yield return new WaitForSeconds(10f * Time.deltaTime);
        }

        moved = true;
        yield return null;
    }
}
