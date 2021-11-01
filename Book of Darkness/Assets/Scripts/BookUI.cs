using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    DimensionController dimension;
    Animator animator;
    Image sprite;
    public Image hotkey;
    public Image meter;

    void Start()
    {
        dimension = DimensionController.instance;
        dimension.DimensionSwitchEvent += Transition;
        Inventory.instance.BookObtainedEvent += DisplayUI;
        animator = GetComponent<Animator>();
        sprite = GetComponent<Image>();

        animator.enabled = false;
        sprite.enabled = false;
        hotkey.enabled = false;
        meter.enabled = false;
    }

    void DisplayUI()
    {
        animator.SetBool("BookObtained", true);

        animator.enabled = true;
        sprite.enabled = true;
        hotkey.enabled = true;
        meter.enabled = true;

        StartCoroutine(FadeIn(sprite));
        StartCoroutine(FadeIn(hotkey));
        StartCoroutine(FadeIn(meter));
    }

    IEnumerator FadeIn(Image img)
    {
        var percent = 0f;
        img.color = Color.clear;

        while (percent < 1)
        {
            percent += Time.deltaTime;
            img.color = Color.Lerp(Color.clear, Color.white, percent);
            yield return null;
        }
    }

    void Transition()
    {
        animator.SetBool("BookOpened", dimension.darkness.enabled);
    }
}
