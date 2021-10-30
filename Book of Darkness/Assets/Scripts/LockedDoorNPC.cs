using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorNPC : NPC
{
    float timer = 0;
    float useTime = 2f;
    Image bar;

    bool usingKey = false;
    public Item key;

    void Update()
    {
        //bar.fillAmount = timer/useTime;
        if (usingKey)
        {
            timer += Time.deltaTime;
            if (timer >= useTime)
            {
                Unlock();
                timer = 0;
                usingKey = false;
                Inventory.instance.Remove(key);
            }
        }
    }

    public override void Interact(Collider2D col)
    {
        if (Inventory.instance.HasItem("Parents Room Key"))
        {
            usingKey = true;
            return;
        }

        Talk(dialogues[1]);
    }

    void Unlock()
    {
        Talk(dialogues[0]);
        // Play SFX
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            base.SetInteracted(false);
            usingKey = false;
        }
    }
}
