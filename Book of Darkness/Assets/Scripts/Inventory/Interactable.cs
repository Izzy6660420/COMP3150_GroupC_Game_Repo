using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject tooltip;
    public float radius = 1f;
    bool hasInteracted = false;

    public virtual void Interact(Collider2D col)
    {
        //For overriding
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && tooltip != null)
            DisplayTooltip(true);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hasInteracted && Input.GetButton(InputAxes.Interact))
        {
            Interact(col);
            hasInteracted = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SetInteracted(false);
        }
    }

    public void SetInteracted(bool b)
    {
        DisplayTooltip(false);
        hasInteracted = b;
    }

    public void DisplayTooltip(bool b)
    {
        tooltip.SetActive(b);
    }
}
