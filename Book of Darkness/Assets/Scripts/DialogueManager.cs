using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of DialogueManager detected!");
        }
        instance = this;
    }

    public Text nameUI;
    public Text contentUI;
    public Animator animator;
    Queue<string> lines;

    void Start()
    {
        lines = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        lines.Clear();
        nameUI.text = dialogue.name;

        foreach (var sentence in dialogue.sentences)
        {
            lines.Enqueue(sentence);
        }

        DisplayNext();
    }

    public void DisplayNext()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        string temp = lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(DisplayText(temp));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    IEnumerator DisplayText(string text)
    {
        contentUI.text = "";
        foreach (var letter in text.ToCharArray())
        {
            contentUI.text += letter;
            yield return null;
        }
    }
}
