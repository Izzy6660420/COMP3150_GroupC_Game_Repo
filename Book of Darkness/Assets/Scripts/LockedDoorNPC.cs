using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorNPC : NPC
{
    public Item key;
    float timer = 0;
    float useTime = 2f;
    public Image bar;

    bool usingKey = false;
    
    void Update()
    {
        bar.enabled = false;
        if (usingKey)
        {
            bar.enabled = true;
            timer += Time.deltaTime;
            if (timer >= useTime)
            {
                Unlock();
                timer = 0;
                usingKey = false;
                Inventory.instance.Remove(key);
            }
        }
        bar.fillAmount = timer / useTime;
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
            timer = 0;
        }
    }
}
