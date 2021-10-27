using UnityEngine;

public class PanicUI : MonoBehaviour
{
    Animator anim;
    PlayerPanic panic;

    void Start()
    {
        panic = PlayerPanic.instance;
        anim = GetComponent<Animator>();
        anim.speed = 0f;
    }

    void Update()
    {
        UpdateMeter(panic.GetPercent());
    }

    void UpdateMeter(float p)
    {
        var n = Mathf.Clamp(p - 0.01f, 0f, 1f);
        anim.Play("PanicMeter", 0, n);
    }
}
