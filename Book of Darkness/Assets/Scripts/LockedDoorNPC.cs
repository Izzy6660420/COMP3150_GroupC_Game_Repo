using System;
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
    public Image screen;

    public Text line1;
    public Text line2;

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
        DialogueManager.instance.EndDialogue();

        StartCoroutine(EndGame());
        
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

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeScreen());
        StartCoroutine(Fade1());
        StartCoroutine(Fade2());
    }

    IEnumerator FadeScreen()
    {
        var percent = 0f;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            screen.color = Color.Lerp(Color.clear, Color.black, percent);
            yield return null;
        }
    }
    IEnumerator Fade1()
    {
        var percent = 0f;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            line1.color = Color.Lerp(Color.clear, Color.white, percent);
            yield return null;
        }
    }
    IEnumerator Fade2()
    {
        var percent = 0f;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            line2.color = Color.Lerp(Color.clear, Color.white, percent);
            yield return null;
        }
    }
}
