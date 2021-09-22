using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        //For overriding
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hasInteracted && Input.GetButton(InputAxes.Interact))
        {
            Interact();
            hasInteracted = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            hasInteracted = false;
        }
    }
}
