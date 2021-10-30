using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorNPC : NPC
{
    float timer = 0;
    float useTime = 2f;

    public override void Interact(Collider2D col)
    {
        var hasKey = Inventory.instance.HasItem("Parents Room Key");
        var i = 1;

        if (true)
        {
            if (Input.GetKey("e"))
            {
                Debug.Log(timer);
                timer += Time.deltaTime;
                if (timer >= useTime)
                    Talk(dialogues[0]);
            }
            else
            {
                timer = 0;
            }
            return;
        }

        Talk(dialogues[i]);
    }
}
