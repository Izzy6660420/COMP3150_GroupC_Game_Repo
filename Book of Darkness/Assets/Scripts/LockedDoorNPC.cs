using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorNPC : NPC
{
    float timer = 0;
    float useTime = 2f;
    Image bar;

    void Update()
    {
        bar.fillAmount = timer/useTime;
    }

    public override void Interact(Collider2D col)
    {
        var hasKey = Inventory.instance.HasItem("Parents Room Key");
        var i = 1;

        if (hasKey) // Interact only called once, may need to create new class without extension from NPC/Interactable
        {
            if (Input.GetKeyDown("e"))
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
