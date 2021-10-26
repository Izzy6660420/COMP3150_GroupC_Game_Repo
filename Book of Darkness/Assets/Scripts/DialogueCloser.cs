using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCloser : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DialogueManager.instance.EndDialogue();
        }
    }
}
