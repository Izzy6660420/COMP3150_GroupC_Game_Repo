using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    DimensionController dimension;
    Animator animator;
    Image sprite;

    void Start()
    {
        dimension = DimensionController.instance;
        dimension.DimensionSwitchEvent += Transition;
        animator = GetComponent<Animator>();
        sprite = GetComponent<Image>();
    }

    void Update()
    {
        sprite.enabled = false;
        if (Inventory.instance.HasItem("Book"))
            sprite.enabled = true;
    }

    void Transition()
    {
        animator.SetBool("BookOpened", dimension.darkness.enabled);
    }
}
