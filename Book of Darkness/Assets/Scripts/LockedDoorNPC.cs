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
    public Sprite unlockedDoor;

    bool usingKey = false;
    bool unlocked = false;
    
    void Update()
    {
        bar.enabled = false;
        if (usingKey)
        {
            bar.enabled = true;
            timer += Time.deltaTime;
            if (timer >= useTime) Unlock();
        }
        bar.fillAmount = timer / useTime;
    }

    public override void Interact(Collider2D col)
    {
        if (unlocked)
        {
            Talk(dialogues[2]);
            return;
        }

        if (Inventory.instance.HasItem("Parents Room Key"))
        {
            AudioManager.instance.PlaySound("Key", transform.position);
            usingKey = true;
            return;
        }

        Talk(dialogues[1]);
    }

    void Unlock()
    {
        timer = 0;
        usingKey = false;
        unlocked = true;
        Inventory.instance.Remove(key);
        GetComponent<SpriteRenderer>().sprite = unlockedDoor;
        AudioManager.instance.PlaySound("Unlock Door", transform.position);
        Talk(dialogues[0], true);
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
