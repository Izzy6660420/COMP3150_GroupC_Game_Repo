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

    void Talk(Dialogue dialogue)
    {
        DialogueManager.instance.StartDialogue(dialogue);
        talkedTo = true;
    }
}
