using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue dialogue;
    bool talkedTo = false;

    public override void Interact(Collider2D col)
    {
        Talk();
    }

    void Talk()
    {
        DialogueManager.instance.StartDialogue(dialogue);
        talkedTo = true;
    }
}
