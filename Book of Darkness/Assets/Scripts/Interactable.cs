using UnityEngine;
using UnityEditor;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        //For overriding
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hasInteracted)
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

    void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), radius);

    }
}
