using UnityEngine;

public class TorchUI : MonoBehaviour
{
    Animator anim;
    PlayerTorch torch;

    void Start()
    {
        torch = PlayerTorch.instance;
        anim = GetComponent<Animator>();
        anim.speed = 0f;
    }

    void Update()
    {
        UpdateMeter(torch.GetPercent());
    }

    void UpdateMeter(float p)
    {
        var n = Mathf.Clamp(p - 0.01f, 0f, 1f);
        anim.Play("BatteryMeter", 0, n);
    }
}
