using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    DimensionController dimension;
    Animator animator;

    void Start()
    {
        dimension = DimensionController.instance;
        dimension.DimensionSwitchEvent += Transition;
        animator = GetComponent<Animator>();
    }

    void Transition()
    {
        animator.SetBool("BookOpened", dimension.darkness.enabled);
    }
}
