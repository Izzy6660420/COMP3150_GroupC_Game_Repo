using UnityEngine;
using UnityEngine.UI;

public class TorchBarUI : MonoBehaviour
{
    Slider torch;
    Image bar;

    void Start()
    {
        torch = TorchUI.instance.slider;
        bar = GetComponent<Image>();
    }

    void Update()
    {
        bar.fillAmount = torch.normalizedValue;
    }
}
