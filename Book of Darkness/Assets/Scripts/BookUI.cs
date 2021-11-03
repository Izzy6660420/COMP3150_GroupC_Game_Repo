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
    public Image hotkeyCover;

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
        hotkeyCover.enabled = false;
    }

    void DisplayUI()
    {
        animator.SetBool("BookObtained", true);
        AudioManager.instance.PlaySound("Book Flip", transform.position);

        animator.enabled = true;
        sprite.enabled = true;
        hotkey.enabled = true;
        hotkeyCover.enabled = true;
        meter.enabled = true;
        hotkeyCover.color = Color.clear;

        StartCoroutine(FadeIn(sprite));
        StartCoroutine(FadeIn(hotkey));
        StartCoroutine(FadeIn(meter));
    }

    IEnumerator FadeIn(Image img, bool fadeOut = false)
    {
        var percent = 0f;
        img.color = fadeOut ? Color.white : Color.clear;
        var initColor = img.color;
        var endColor = fadeOut ? Color.clear : Color.white;

        while (percent < 1)
        {
            percent += Time.deltaTime;
            img.color = Color.Lerp(initColor, endColor, percent);
            yield return null;
        }
    }

    public void PulseHotkey()
    {
        StartCoroutine(FadeIn(hotkeyCover, true));
    }

    void Transition()
    {
        animator.SetBool("BookOpened", dimension.darkness.enabled);
    }
}
