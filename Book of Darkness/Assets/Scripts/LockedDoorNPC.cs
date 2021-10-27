using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorNPC : NPC
{
    public override void Interact(Collider2D col)
    {
        var hasKey = Inventory.instance.HasItem("Parent's Room Key");
        var i = hasKey ? 0 : 1;
        Talk(dialogues[i]);
    }
}
