using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject tooltip;
    public float radius = 1f;
    bool hasInteracted = false;
    Animator animator;
    float tooltipTimer;

    public virtual void Interact(Collider2D col)
    {
        //For overriding
    }

    void Start()
    {
        if (tooltip == null)
            return;
        animator = tooltip.GetComponent<Animator>();
        tooltipTimer = animator.runtimeAnimatorController.animationClips[0].length; // NOT WORKING, NEED TO FIND METHOD TO RETRIEVE ANIMATION LENGTH
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
