using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue[] dialogues;
    public bool talkedTo;

    public override void Interact(Collider2D col)
    {
        
    }

    public void Talk(Dialogue dialogue, bool door = false)
    {
        DialogueManager.instance.StartDialogue(dialogue, door);
        talkedTo = true;
    }
}
